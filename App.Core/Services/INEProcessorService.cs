using App.Core.Interfaces.Core;
using System.Drawing;
using System.Text.RegularExpressions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Tesseract;
using AutoMapper;

namespace App.Core.Services
{
    public class INEProcessorService : IINEProcessorService
    {
        private readonly string _tessdataPath;
        private readonly string _language;
        protected readonly IManagerService managerGenericService;
        public readonly IMapper mapper;

        // Regions of interest for different INE versions
        private readonly Dictionary<string, Dictionary<string, Rectangle>> _roiMappings;

        public INEProcessorService(IManagerService managerGenericService, IMapper mapper, string tessdataPath = "./tessdata", string language = "spa")
        {
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
            _tessdataPath = tessdataPath;
            _language = language;

            // Define regions of interest for different INE versions
            _roiMappings = new Dictionary<string, Dictionary<string, Rectangle>>
            {
                {
                    "INE_2019", new Dictionary<string, Rectangle>
                    {
                        { "full_name", new Rectangle(220, 195, 500, 40) },
                        { "address", new Rectangle(220, 238, 500, 60) },
                        { "curp", new Rectangle(220, 305, 300, 30) },
                        { "voter_key", new Rectangle(220, 340, 300, 30) },
                        { "state", new Rectangle(220, 375, 150, 30) },
                        { "municipality", new Rectangle(400, 375, 170, 30) },
                        { "electoral_section", new Rectangle(600, 375, 100, 30) },
                        { "expiration_date", new Rectangle(600, 340, 150, 30) }
                    }
                },
                {
                    "INE_2013", new Dictionary<string, Rectangle>
                    {
                        { "full_name", new Rectangle(240, 180, 450, 40) },
                        { "address", new Rectangle(240, 225, 450, 60) },
                        { "curp", new Rectangle(240, 290, 300, 30) },
                        { "voter_key", new Rectangle(240, 325, 280, 30) },
                        { "state", new Rectangle(240, 360, 130, 30) },
                        { "municipality", new Rectangle(380, 360, 170, 30) },
                        { "electoral_section", new Rectangle(580, 360, 100, 30) },
                        { "expiration_date", new Rectangle(580, 325, 150, 30) }
                    }
                },
                {
                    "IFE_2008", new Dictionary<string, Rectangle>
                    {
                        { "full_name", new Rectangle(250, 170, 430, 40) },
                        { "address", new Rectangle(250, 215, 430, 60) },
                        { "curp", new Rectangle(250, 280, 280, 30) },
                        { "voter_key", new Rectangle(250, 315, 260, 30) },
                        { "state", new Rectangle(250, 350, 120, 30) },
                        { "municipality", new Rectangle(380, 350, 160, 30) },
                        { "electoral_section", new Rectangle(550, 350, 90, 30) },
                        { "expiration_date", new Rectangle(550, 315, 140, 30) }
                    }
                }
            };
        }

        /// <summary>
        /// Detects the type of INE based on visual features.
        /// </summary>
        private static string DetectINEType(Mat image)
        {
            using var grayImage = new Mat();
            CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

            // Look for the INE 2019 logo (top-right corner)
            var roiLogo2019 = new Rectangle(image.Width - 180, 10, 150, 60);
            using var logoRoi2019 = new Mat(grayImage, roiLogo2019);

            // Look for the INE 2013 logo (top-left corner)
            var roiLogo2013 = new Rectangle(20, 10, 150, 60);
            using var logoRoi2013 = new Mat(grayImage, roiLogo2013);

            // Detect based on dominant color and logo position
            double meanIntensity2019 = CvInvoke.Mean(logoRoi2019).V0;
            double meanIntensity2013 = CvInvoke.Mean(logoRoi2013).V0;

            if (meanIntensity2019 < 100) // Darker logo in the right corner
                return "INE_2019";
            else if (meanIntensity2013 < 100)
                return "INE_2013";
            else
                return "IFE_2008"; // Default to the oldest version
        }

        /// <summary>
        /// Preprocesses the image to improve OCR accuracy.
        /// </summary>
        private static Mat PreprocessImage(Mat originalImage)
        {
            // Convert to grayscale
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(originalImage, grayImage, ColorConversion.Bgr2Gray);

            // Enhance contrast with histogram equalization
            Mat enhancedImage = new Mat();
            CvInvoke.EqualizeHist(grayImage, enhancedImage);

            // Apply adaptive thresholding for binarization
            Mat binaryImage = new Mat();
            CvInvoke.AdaptiveThreshold(
                enhancedImage,
                binaryImage,
                255,
                AdaptiveThresholdType.GaussianC,
                ThresholdType.Binary,
                11,
                2);

            // Reduce noise
            Mat denoisedImage = new Mat();
            CvInvoke.MedianBlur(binaryImage, denoisedImage, 3);

            return denoisedImage;
        }

        /// <summary>
        /// Corrects the orientation of the image if necessary.
        /// </summary>
        private static Mat CorrectOrientation(Mat image)
        {
            // Detect edges
            Mat edges = new Mat();
            CvInvoke.Canny(image, edges, 50, 150);

            // Detect lines using Hough transform
            LineSegment2D[] lines = CvInvoke.HoughLinesP(
                edges,
                1,
                Math.PI / 180,
                100,
                100,
                10);

            if (lines.Length == 0)
                return image.Clone();

            // Calculate average angle
            double sumAngles = 0;
            int validLines = 0;

            foreach (var line in lines)
            {
                if (Math.Abs(line.P2.X - line.P1.X) > 50) // Long horizontal lines
                {
                    double angle = Math.Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X) * 180 / Math.PI;
                    sumAngles += angle;
                    validLines++;
                }
            }

            if (validLines == 0)
                return image.Clone();

            double averageAngle = sumAngles / validLines;

            // Correct rotation
            Mat rotationMatrix = new Mat();
            PointF center = new PointF(image.Width / 2f, image.Height / 2f);

            // Modificar esta línea para usar la nueva firma del método
            CvInvoke.GetRotationMatrix2D(center, averageAngle, 1.0, rotationMatrix);

            Mat rotatedImage = new Mat();
            CvInvoke.WarpAffine(
                image,
                rotatedImage,
                rotationMatrix,
                image.Size);

            return rotatedImage;
        }

        /// <summary>
        /// Processes an INE image and extracts the relevant information.
        /// </summary>
        //public async Task<Dictionary<string, string>> ProcessINEImageAsync(string imagePath)
        //{
        //    var result = new Dictionary<string, string>();

        //    try
        //    {
        //        // Carga de imagen - podemos hacerla asíncrona
        //        byte[] imageData = await File.ReadAllBytesAsync(imagePath);

        //        // Convertir los bytes a Mat
        //        using var stream = new MemoryStream(imageData);
        //        using var originalImage = new Mat();
        //        CvInvoke.Imdecode(imageData, ImreadModes.Color, originalImage);

        //        if (originalImage.IsEmpty)
        //            throw new FileNotFoundException("Could not load the image", imagePath);

        //        // Operaciones CPU-intensivas se pueden ejecutar en un thread separado
        //        using var correctedImage = await Task.Run(() => CorrectOrientation(originalImage));

        //        // Detect INE type
        //        string ineType = await Task.Run(() => DetectINEType(correctedImage));
        //        result.Add("ine_type", ineType);

        //        // Get regions of interest for this INE type
        //        if (!_roiMappings.TryGetValue(ineType, out var regionMappings))
        //            throw new InvalidOperationException($"No mappings found for INE type: {ineType}");

        //        // Preprocess the image (CPU-intensivo)
        //        using var preprocessedImage = await Task.Run(() => PreprocessImage(correctedImage));

        //        // Configure OCR engine
        //        using var engine = new TesseractEngine(_tessdataPath, _language, EngineMode.LstmOnly);
        //        engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ0123456789-/");

        //        // Process each region
        //        foreach (var region in regionMappings)
        //        {
        //            string fieldName = region.Key;
        //            Rectangle roi = region.Value;

        //            // Extract region from the image
        //            using var roiImage = new Mat(preprocessedImage, roi);

        //            // Field-specific configurations
        //            switch (fieldName)
        //            {
        //                case "curp":
        //                    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        //                    break;
        //                case "voter_key":
        //                    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        //                    break;
        //                case "expiration_date":
        //                    engine.SetVariable("tessedit_char_whitelist", "0123456789-/");
        //                    break;
        //                default:
        //                    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ0123456789,.- ");
        //                    break;
        //            }

        //            // Save temporarily for Tesseract processing
        //            string tempFile = Path.GetTempFileName() + ".png";
        //            await Task.Run(() => roiImage.Save(tempFile));

        //            // Process with Tesseract (CPU-intensivo)
        //            using var img = await Task.Run(() => Pix.LoadFromFile(tempFile));
        //            using var page = await Task.Run(() => engine.Process(img));
        //            string text = page.GetText().Trim();

        //            // Store the result
        //            result.Add(fieldName, text);

        //            // Delete the temporary file
        //            await Task.Run(() => File.Delete(tempFile));
        //        }

        //        // Validate and clean the data
        //        await Task.Run(() => CleanAndValidateData(result));

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error processing INE image: {ex.Message}", ex);
        //    }
        //}
        public async Task<Dictionary<string, string>> ProcessINEImageAsync(string imagePath)
        {
            var result = new Dictionary<string, string>();

            try
            {
                // Carga de imagen
                byte[] imageData = await File.ReadAllBytesAsync(imagePath);

                // Convertir los bytes a Mat
                using var stream = new MemoryStream(imageData);
                using var originalImage = new Mat();
                CvInvoke.Imdecode(imageData, ImreadModes.Color, originalImage);

                if (originalImage.IsEmpty)
                    throw new FileNotFoundException("Could not load the image", imagePath);

                // Operaciones CPU-intensivas se pueden ejecutar en un thread separado
                using var correctedImage = await Task.Run(() => CorrectOrientation(originalImage));

                // Detect INE type
                string ineType = await Task.Run(() => DetectINEType(correctedImage));
                result.Add("ine_type", ineType);

                // Get regions of interest for this INE type
                if (!_roiMappings.TryGetValue(ineType, out var regionMappings))
                    throw new InvalidOperationException($"No mappings found for INE type: {ineType}");

                // Preprocess the image (CPU-intensivo)
                using var preprocessedImage = await Task.Run(() => PreprocessImage(correctedImage));

                // Configurar motor OCR
                using var engine = new TesseractEngine(_tessdataPath, _language, EngineMode.LstmOnly);
                engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ0123456789-/");

                // Proceso de cada región con manejo de errores
                foreach (var region in regionMappings)
                {
                    string fieldName = region.Key;
                    Rectangle roi = region.Value;

                    try
                    {
                        // Extraer región de la imagen usando el nuevo método
                        using var roiImage = ExtractROI(preprocessedImage, roi);

                        // Configuraciones específicas por campo
                        switch (fieldName)
                        {
                            case "curp":
                                engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                                break;
                            case "voter_key":
                                engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                                break;
                            case "expiration_date":
                                engine.SetVariable("tessedit_char_whitelist", "0123456789-/");
                                break;
                            default:
                                engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ0123456789,.- ");
                                break;
                        }

                        // Guardar temporalmente para procesamiento de Tesseract
                        string tempFile = Path.GetTempFileName() + ".png";
                        await Task.Run(() => roiImage.Save(tempFile));

                        // Procesar con Tesseract (CPU-intensivo)
                        using var img = await Task.Run(() => Pix.LoadFromFile(tempFile));
                        using var page = await Task.Run(() => engine.Process(img));
                        string text = page.GetText().Trim();

                        // Almacenar el resultado
                        result.Add(fieldName, text);

                        // Eliminar archivo temporal
                        await Task.Run(() => File.Delete(tempFile));
                    }
                    catch (Exception fieldEx)
                    {
                        Console.WriteLine($"Error procesando campo {fieldName}: {fieldEx.Message}");
                        // Opcional: añadir el campo con un valor vacío o un mensaje de error
                        result.Add(fieldName, $"Error al procesar: {fieldEx.Message}");
                    }
                }

                // Limpiar y validar datos
                await Task.Run(() => CleanAndValidateData(result));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing INE image: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cleans and validates the extracted data.
        /// </summary>
        private void CleanAndValidateData(Dictionary<string, string> data)
        {
            foreach (var key in data.Keys.ToArray())
            {
                data[key] = data[key].Replace("\n", " ").Replace("\r", "").Trim();

                // Apply field-specific validations
                switch (key)
                {
                    case "curp":
                        var curpRegex = new Regex(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]\d$");
                        if (!curpRegex.IsMatch(data[key]))
                            data[key] = CleanupCURP(data[key]);
                        break;
                    case "voter_key":
                        var voterKeyRegex = new Regex(@"^[A-Z]{6}\d{8}[HM][A-Z]{3}$");
                        if (!voterKeyRegex.IsMatch(data[key]))
                            data[key] = CleanupVoterKey(data[key]);
                        break;
                    case "expiration_date":
                        data[key] = CleanupDate(data[key]);
                        break;
                }
            }
        }

        /// <summary>
        /// Cleans and corrects common errors in the CURP.
        /// </summary>
        private string CleanupCURP(string curp)
        {
            string cleaned = Regex.Replace(curp, @"[^A-Z0-9]", "");
            cleaned = cleaned.Replace("O", "0").Replace("I", "1").Replace("Z", "2");

            var match = Regex.Match(cleaned, @"([A-Z]{4})(\d{6})([HM])?([A-Z]{0,5})(\d{0,2})");
            if (match.Success)
            {
                string letters1 = match.Groups[1].Value.PadRight(4, 'X');
                string numbers = match.Groups[2].Value.PadRight(6, '0');
                string gender = match.Groups[3].Value;
                if (string.IsNullOrEmpty(gender)) gender = "H";
                string letters2 = match.Groups[4].Value.PadRight(5, 'X');
                string final = match.Groups[5].Value.PadRight(2, '0');

                return $"{letters1}{numbers}{gender}{letters2}{final}";
            }

            return cleaned;
        }

        private Mat ExtractROI(Mat image, Rectangle roi)
        {
            // Validaciones de seguridad
            if (image == null || image.IsEmpty)
            {
                throw new ArgumentException("Imagen de entrada es nula o vacía");
            }

            // Verificar límites de la región de interés
            if (roi.X < 0 || roi.Y < 0 ||
                roi.X + roi.Width > image.Width ||
                roi.Y + roi.Height > image.Height)
            {
                // Ajustar la región de interés para que esté dentro de los límites de la imagen
                int adjustedX = Math.Max(0, roi.X);
                int adjustedY = Math.Max(0, roi.Y);
                int adjustedWidth = Math.Min(roi.Width, image.Width - adjustedX);
                int adjustedHeight = Math.Min(roi.Height, image.Height - adjustedY);

                var adjustedRoi = new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight);

                // Log de ajuste
                Console.WriteLine($"ROI ajustada: Original {roi}, Ajustada {adjustedRoi}");

                return new Mat(image, adjustedRoi);
            }

            // Si los límites son válidos, extraer normalmente
            return new Mat(image, roi);
        }

        /// <summary>
        /// Cleans and corrects common errors in the voter key.
        /// </summary>
        private string CleanupVoterKey(string voterKey)
        {
            string cleaned = Regex.Replace(voterKey, @"[^A-Z0-9]", "");
            cleaned = cleaned.Replace("O", "0").Replace("I", "1");

            var match = Regex.Match(cleaned, @"([A-Z]{0,6})(\d{0,8})([HM])?([A-Z]{0,3})");
            if (match.Success)
            {
                string letters1 = match.Groups[1].Value.PadRight(6, 'X');
                string numbers = match.Groups[2].Value.PadRight(8, '0');
                string gender = match.Groups[3].Value;
                if (string.IsNullOrEmpty(gender)) gender = "H";
                string letters2 = match.Groups[4].Value.PadRight(3, 'X');

                return $"{letters1}{numbers}{gender}{letters2}";
            }

            return cleaned;
        }

        /// <summary>
        /// Cleans and corrects date formats.
        /// </summary>
        private string CleanupDate(string date)
        {
            var match = Regex.Match(date, @"(\d{1,4})[-/\s]*(\d{1,2})[-/\s]*(\d{1,4})");
            if (match.Success)
            {
                string part1 = match.Groups[1].Value;
                string part2 = match.Groups[2].Value;
                string part3 = match.Groups[3].Value;

                if (part1.Length == 4 && int.Parse(part1) > 1900)
                    return $"{part1}-{part2.PadLeft(2, '0')}-{part3.PadLeft(2, '0')}";

                if (part3.Length == 4 && int.Parse(part3) > 1900)
                    return $"{part3}-{part2.PadLeft(2, '0')}-{part1.PadLeft(2, '0')}";

                return $"{part1.PadLeft(2, '0')}/{part2.PadLeft(2, '0')}/{part3.PadLeft(4, '0')}";
            }

            return date;
        }
    }
}
