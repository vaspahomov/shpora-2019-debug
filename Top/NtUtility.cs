using System;

namespace Top
{
    internal static class NtUtility
    {
        public static unsafe void VisitProcesses(SystemProcInfoVisitor visit)
        {
            var size = 1024;

            while (true)
            {
                using (var releaser = ByteArrayPool.Instance.Acquire(size))
                {
                    var buffer = releaser.Value;
                    
                    fixed (byte* ptr = buffer)
                    {
                        var status = NtDll.NtQuerySystemInformation(
                            SYSTEM_INFORMATION_CLASS.SystemProcessInformation,
                            (IntPtr) ptr,
                            buffer.Length,
                            out size);
                        if (status == NtStatus.STATUS_INFO_LENGTH_MISMATCH)
                            continue;

                        if (!status.IsSuccessful())
                            throw new InvalidOperationException(
                                $"Can't query SystemProcessInformation. NtStatus: 0x{status:X}");

                        VisitProcesses(ptr, visit);
                        return;
                    }
                }
            }
        }

        private static unsafe void VisitProcesses(byte* ptr, SystemProcInfoVisitor visit)
        {
            while (true)
            {
                var processInfo = (SystemProcessInformation*) ptr;
                
                if (!IsZombie(processInfo))
                    visit(processInfo);

                if (processInfo->NextEntryOffset == 0)
                    return;
                
                ptr += processInfo->NextEntryOffset;
            }
        }

        private static unsafe bool IsZombie(SystemProcessInformation* info) => info->NumberOfThreads == 0;

        internal unsafe delegate void SystemProcInfoVisitor(SystemProcessInformation* info);
    }
}