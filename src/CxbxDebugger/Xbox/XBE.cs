// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using System.IO;
using System.Text;

namespace CxbxDebugger.Xbox
{
    class XBE
    {
        private Header header;
        private Certificate certificate;

        /// <summary>
        /// Extracted path from metadata
        /// </summary>
        public string DebugPath { get; private set; } = "";
        /// <summary>
        /// Extracted filename from metadata
        /// </summary>
        public string DebugFilename { get; private set; } = "";
        /// <summary>
        /// Formatted title id (in the format XX-123)
        /// </summary>
        public string TitleId { get; private set; } = "<unknown>";
        /// <summary>
        /// Formatted title id (as hex)
        /// </summary>
        public string TitleIdHex { get; private set; } = "";
        /// <summary>
        /// Extracted title string from metadata
        /// </summary>
        public string Title
        {
            get
            {
                return certificate.wszTitleName;
            }
        }

        static string CleanString(string str)
        {
            var nullt = str.IndexOf('\0');
            if (nullt != -1)
            {
                return str.Substring(0, nullt);
            }

            return str;
        }

        static void ReaderHeader(BinaryReader stream, ref Header header)
        {
            header.dwMagic = stream.ReadUInt32();
            header.pbDigitalSignature = stream.ReadBytes(256);
            header.dwBaseAddr = stream.ReadUInt32();
            header.dwSizeofHeaders = stream.ReadUInt32();
            header.dwSizeofImage = stream.ReadUInt32();
            header.dwSizeofImageHeader = stream.ReadUInt32();
            header.dwTimeDate = stream.ReadUInt32();
            header.dwCertificateAddr = stream.ReadUInt32();
            header.dwSections = stream.ReadUInt32();
            header.dwSectionHeadersAddr = stream.ReadUInt32();
            header.dwInitFlags = stream.ReadUInt32();
            header.dwEntryAddr = stream.ReadUInt32();
            header.dwTLSAddr = stream.ReadUInt32();
            header.dwPeStackCommit = stream.ReadUInt32();
            header.dwPeHeapReserve = stream.ReadUInt32();
            header.dwPeHeapCommit = stream.ReadUInt32();
            header.dwPeBaseAddr = stream.ReadUInt32();
            header.dwPeSizeofImage = stream.ReadUInt32();
            header.dwPeChecksum = stream.ReadUInt32();
            header.dwPeTimeDate = stream.ReadUInt32();
            header.dwDebugPathnameAddr = stream.ReadUInt32();
            header.dwDebugFilenameAddr = stream.ReadUInt32();
            header.dwDebugUnicodeFilenameAddr = stream.ReadUInt32();
            header.dwKernelImageThunkAddr = stream.ReadUInt32();
            header.dwNonKernelImportDirAddr = stream.ReadUInt32();
            header.dwLibraryVersions = stream.ReadUInt32();
            header.dwLibraryVersionsAddr = stream.ReadUInt32();
            header.dwKernelLibraryVersionAddr = stream.ReadUInt32();
            header.dwXAPILibraryVersionAddr = stream.ReadUInt32();
            header.dwLogoBitmapAddr = stream.ReadUInt32();
            header.dwSizeofLogoBitmap = stream.ReadUInt32();
        }

        static void ReadCertificate(BinaryReader stream, ref Certificate certificate)
        {
            certificate.dwSize = stream.ReadUInt32();
            certificate.dwTimeDate = stream.ReadUInt32();
            certificate.dwTitleId = stream.ReadUInt32();
            certificate.wszTitleName = CleanString(Encoding.Unicode.GetString(stream.ReadBytes(80)));

            certificate.dwAlternateTitleId = new uint[10];
            var altTitleIds = stream.ReadBytes(10 * 4);
            for (int i = 0; i < 10; ++i)
            {
                certificate.dwAlternateTitleId[i] = BitConverter.ToUInt32(altTitleIds, i * 4);
            }

            certificate.dwAllowedMedia = stream.ReadUInt32();
            certificate.dwGameRegion = stream.ReadUInt32();
            certificate.dwGameRatings = stream.ReadUInt32();
            certificate.dwDiskNumber = stream.ReadUInt32();
            certificate.dwVersion = stream.ReadUInt32();

            certificate.bzLanKey = stream.ReadBytes(16);
            certificate.bzSignatureKey = stream.ReadBytes(16);

            // NOT ALL XBEs have these fields!
            certificate.bzTitleAlternateSignatureKey = stream.ReadBytes(16 * 16);
            certificate.dwOriginalCertificateSize = stream.ReadUInt32();
            certificate.dwOnlineService = stream.ReadUInt32();
            certificate.dwSecurityFlags = stream.ReadUInt32();
            certificate.bzCodeEncKey = stream.ReadBytes(16);
        }

        private string GetString(BinaryReader stream, uint address, int length, Encoding encoding)
        {
            if (address < header.dwBaseAddr)
                return string.Empty;

            var offset = address - header.dwBaseAddr;
            stream.BaseStream.Seek(offset, SeekOrigin.Begin);

            var str = encoding.GetString(stream.ReadBytes(length));

            return CleanString(str);
        }

        private string MakeFormattedTitleId()
        {
            var title1 = (char)((certificate.dwTitleId >> 24) & 0xFF);
            var title2 = (char)((certificate.dwTitleId >> 16) & 0xFF);

            if (char.IsLetter(title1) && char.IsLetter(title2))
            {
                var titleCount = (ushort)(certificate.dwTitleId & 0xFFFF);
                return string.Format("{0}{1}-{2:000}", title1, title2, titleCount);
            }

            return "<unknown>";
        }

        private string MakeHexTitleId()
        {
            return certificate.dwTitleId.ToString("x");
        }

        public bool Read(BinaryReader stream)
        {
            ReaderHeader(stream, ref header);

            if (header.dwMagic != Header.Magic)
                return false;

            stream.BaseStream.Seek(header.dwCertificateAddr - header.dwBaseAddr, SeekOrigin.Begin);
            ReadCertificate(stream, ref certificate);

            DebugPath = GetString(stream, header.dwDebugPathnameAddr, 100, Encoding.UTF8);
            DebugFilename = GetString(stream, header.dwDebugUnicodeFilenameAddr, 100, Encoding.Unicode);

            TitleId = MakeFormattedTitleId();
            TitleIdHex = MakeHexTitleId();

            return true;
        }
    }
}
