using FellowOakDicom;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.NativeCodec;
using System.IO;
using System.Reflection;

new DicomSetupBuilder()
    .RegisterServices(s => s.AddTranscoderManager<NativeTranscoderManager>())
    .Build();

for (int i = 1; i < 6; i++)
{
    string path = Assembly.GetExecutingAssembly().Location;
    string directory = Path.GetDirectoryName(path);
    string filePath = Path.Combine(directory, "50mb.dcm");
    DicomFile dicomFile = DicomFile.Open(filePath);
    DicomDataset result;
    try
    {
        var transcode = new DicomTranscoder(dicomFile.Dataset.InternalTransferSyntax, DicomTransferSyntax.JPEG2000Lossless);
        result = transcode.Transcode(dicomFile.Dataset);
        result = null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Error: {ex.Message}");
        return;
    }
}

Console.ReadLine();
