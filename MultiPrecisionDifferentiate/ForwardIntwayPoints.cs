using MultiPrecision;
using System.Collections.ObjectModel;
using System.IO.Compression;

namespace MultiPrecisionDifferentiate {
    public static class ForwardIntwayPoints<N> where N : struct, IConstant {
        public const int MaxDerivative = 64;

        public static ReadOnlyDictionary<int, ReadOnlyCollection<MultiPrecision<N>>> Table { private set; get; }

        static ForwardIntwayPoints() {
            using MemoryStream stream = new();
            using (MemoryStream resorce = new(Resource.forward_intway_acc64)) {
                using GZipStream decompressor = new(resorce, CompressionMode.Decompress);

                decompressor.CopyTo(stream);
            }

            stream.Position = 0;

            Dictionary<int, ReadOnlyCollection<MultiPrecision<N>>> table = new();

            using (BinaryReader sr = new(stream)) {
                for (int n = 1; n <= MaxDerivative; n++) {
                    int ns = sr.ReadInt32();
                    if (n != ns) {
                        throw new IOException("The format of resource file is invalid.");
                    }

                    int pts = sr.ReadInt32();

                    List<MultiPrecision<N>> vals = new();

                    for (int i = 0; i < pts; i++) {
                        string val = sr.ReadString();
                        string[] val_split = val.Split('/');

                        MultiPrecision<N> w = val_split.Length == 1
                            ? val_split[0]
                            : MultiPrecision<N>.Div(val_split[0], val_split[1]);

                        vals.Add(w);
                    }

                    table.Add(n, Array.AsReadOnly(vals.ToArray()));
                }
            }

            Table = new ReadOnlyDictionary<int, ReadOnlyCollection<MultiPrecision<N>>>(table);
        }
    }
}
