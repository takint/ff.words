namespace ff.words.application.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Byte to Array Converter Class
    /// </summary>
    /// /// <remarks>
    /// <see cref="http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa/24343727#24343727" />
    /// </remarks>
    public static class ByteArrayConverter
    {
        /// <summary>
        /// The string index.
        /// </summary>
        private static readonly uint[] StringIndex = CreateStringLookup();

        /// <summary>
        /// The hex index.
        /// </summary>
        private static readonly Dictionary<string, byte> HexIndex = CreateHexLookup();

        /// <summary>
        /// Converts byte array into a string
        /// </summary>
        /// <param name="bytes">Array of bytes to convert</param>
        /// <returns>String of hex values. For example: <c>000102</c> for [0,1,2]</returns>
        public static string ToString(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            var result = new char[bytes.Length * 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                var val = StringIndex[bytes[i]];
                result[2 * i] = (char)val;
                result[(2 * i) + 1] = (char)(val >> 16);
            }

            return new string(result);
        }

        /// <summary>
        /// Converts a hex string back into a byte array
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns>Bytes. For example: [0,1,2] from <c>000102</c></returns>
        /// ERROR: Missing FF in dictionary
        public static byte[] FromString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            var hexres = new List<byte>();

            for (var i = 0; i < str.Length; i += 2)
            {
                hexres.Add(HexIndex[str.Substring(i, 2)]);
            }

            return hexres.ToArray();
        }

        /// <summary>
        /// Creates string lookup.
        /// </summary>
        /// <returns> The <see cref="uint[]"/>.</returns>
        private static uint[] CreateStringLookup()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }

            return result;
        }

        /// <summary>
        /// Creates hex lookup.
        /// </summary>
        /// <returns>The <see cref="Dictionary"/>.</returns>
        private static Dictionary<string, byte> CreateHexLookup()
        {
            var hexindex = new Dictionary<string, byte>();

            for (byte i = 0; i < 255; i++)
            {
                hexindex.Add(i.ToString("X2"), i);
            }

            // Adding missing value FF which was causing random errors.
            hexindex.Add("FF", 255);

            return hexindex;
        }
    }
}