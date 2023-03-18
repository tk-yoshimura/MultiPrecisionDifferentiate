using System.IO.Compression;

const string dirpath_src = "../../../../way_points/";
const string dirpath_dst = "../../../../MultiPrecisionDifferentiate/Resource/";

foreach (string filepath in Directory.EnumerateFiles(dirpath_src, "*.md")) {
    string filename = Path.GetFileNameWithoutExtension(filepath);

    byte[] data;

    using MemoryStream sms = new();
    using (BinaryWriter sws = new(sms)) {
        using StreamReader sfs = new(filepath);

        sfs.ReadLine();
        sfs.ReadLine();

        int n = 1;

        while (!sfs.EndOfStream) {
            string? line = sfs.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) {
                break;
            }

            string[] line_split = line.Split('|', StringSplitOptions.RemoveEmptyEntries).Skip(2).ToArray();

            int takes;
            for (takes = line_split.Length; takes >= 1; takes--) {
                if (line_split[takes - 1] != "0") {
                    break;
                }
            }

            string[] vals = line_split.Take(takes).ToArray();

            sws.Write(n);
            sws.Write(vals.Length);
            foreach (string val in vals) {
                sws.Write(val);
            }

            n++;
        }

        sws.Flush();
        sms.Position = 0;

        data = new byte[sms.Length];
        sms.Read(data, 0, checked((int)sms.Length));
    }

    using FileStream dfs = File.Create(dirpath_dst + filename + ".bin");

    using var compressor = new GZipStream(dfs, CompressionLevel.SmallestSize);

    compressor.Write(data, 0, data.Length);
}