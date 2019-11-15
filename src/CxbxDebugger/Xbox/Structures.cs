// Written by x1nixmzeng for the Cxbx-Reloaded project
//

namespace CxbxDebugger.Xbox
{
    // Ported from xbe.h

    struct Header
    {
        public static uint Magic = 0x48454258;		// "XBEH"

        public uint dwMagic;                        // 0x0000 - magic number [should be "XBEH"]
        public byte[] pbDigitalSignature;			// 0x0004 - digital signature
        public uint dwBaseAddr;                     // 0x0104 - base address
        public uint dwSizeofHeaders;                // 0x0108 - size of headers
        public uint dwSizeofImage;                  // 0x010C - size of image
        public uint dwSizeofImageHeader;            // 0x0110 - size of image header
        public uint dwTimeDate;                     // 0x0114 - timedate stamp
        public uint dwCertificateAddr;              // 0x0118 - certificate address
        public uint dwSections;                     // 0x011C - number of sections
        public uint dwSectionHeadersAddr;           // 0x0120 - section headers address
        public uint dwInitFlags;					// 0x0124 - initialization flags
        public uint dwEntryAddr;                    // 0x0128 - entry point address
        public uint dwTLSAddr;                      // 0x012C - thread local storage directory address
        public uint dwPeStackCommit;                // 0x0130 - size of stack commit
        public uint dwPeHeapReserve;                // 0x0134 - size of heap reserve
        public uint dwPeHeapCommit;                 // 0x0138 - size of heap commit
        public uint dwPeBaseAddr;                   // 0x013C - original base address
        public uint dwPeSizeofImage;                // 0x0140 - size of original image
        public uint dwPeChecksum;                   // 0x0144 - original checksum
        public uint dwPeTimeDate;                   // 0x0148 - original timedate stamp
        public uint dwDebugPathnameAddr;            // 0x014C - debug pathname address
        public uint dwDebugFilenameAddr;            // 0x0150 - debug filename address
        public uint dwDebugUnicodeFilenameAddr;     // 0x0154 - debug unicode filename address
        public uint dwKernelImageThunkAddr;         // 0x0158 - kernel image thunk address
        public uint dwNonKernelImportDirAddr;       // 0x015C - non kernel import directory address
        public uint dwLibraryVersions;              // 0x0160 - number of library versions
        public uint dwLibraryVersionsAddr;          // 0x0164 - library versions address
        public uint dwKernelLibraryVersionAddr;     // 0x0168 - kernel library version address
        public uint dwXAPILibraryVersionAddr;       // 0x016C - xapi library version address
        public uint dwLogoBitmapAddr;               // 0x0170 - logo bitmap address
        public uint dwSizeofLogoBitmap;             // 0x0174 - logo bitmap size
    };

    struct Certificate
    {
        public uint dwSize;							// 0x0000 - size of certificate
        public uint dwTimeDate;                     // 0x0004 - timedate stamp
        public uint dwTitleId;                      // 0x0008 - title id
        public string wszTitleName;					// 0x000C - title name (unicode)
        public uint[] dwAlternateTitleId;			// 0x005C - alternate title ids
        public uint dwAllowedMedia;                 // 0x009C - allowed media types
        public uint dwGameRegion;                   // 0x00A0 - game region
        public uint dwGameRatings;                  // 0x00A4 - game ratings
        public uint dwDiskNumber;                   // 0x00A8 - disk number
        public uint dwVersion;                      // 0x00AC - version
        public byte[] bzLanKey;                     // 0x00B0 - lan key
        public byte[] bzSignatureKey;				// 0x00C0 - signature key
        // NOT ALL XBEs have these fields!
        public byte[] bzTitleAlternateSignatureKey;	// 0x00D0 - alternate signature keys
        public uint dwOriginalCertificateSize;      // 0x01D0 - Original Certificate Size?
        public uint dwOnlineService;                // 0x01D4 - Online Service ID
        public uint dwSecurityFlags;                // 0x01D8 - Extra Security Flags
        public byte[] bzCodeEncKey;                 // 0x01DC - Code Encryption Key?
    }
}
