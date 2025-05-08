namespace App.Core.Interfaces.Core
{
    public interface IINEProcessorService
    {
        /// <summary>
        /// Processes an INE image and extracts the relevant information.
        /// </summary>
        /// <param name="imagePath">The path to the INE image file.</param>
        /// <returns>A dictionary containing the extracted information.</returns>
        Task<Dictionary<string, string>> ProcessINEImageAsync(string imagePath);
    }
}
