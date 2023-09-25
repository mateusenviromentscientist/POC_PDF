using POC_PDF.Services.Interfaces;

namespace POC_PDF.Services;

public class TextTemplateService : ITextTemplate
{
    private const int RetryCount = 3;

    private const int WaitBetweenRetry = 1000;
    public async Task<string> RetrieveAsync(string name)
    {
        var template = "";

        for (var i = 1; i <= RetryCount; i++)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
                var filePath = Path.Combine(dir, $"{name}.cshtml");

                await using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: 4096, useAsync: true);
                using StreamReader reader = new StreamReader(sourceStream);
                template = await reader.ReadToEndAsync();
                break;
            }
            catch (Exception e)
            {
                Thread.Sleep(WaitBetweenRetry);
            }
        }
        return template;
    }
}