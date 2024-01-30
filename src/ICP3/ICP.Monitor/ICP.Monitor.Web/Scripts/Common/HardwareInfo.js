/*
Win32_Processor                        // CPU 处理器

Win32_PhysicalMemory                   // 物理内存

Win32_Keyboard                         // 键盘

Win32_PointingDevice                   // 点输入设备，如鼠标

Win32_DiskDrive                        // 硬盘驱动器

Win32_CDROMDrive                       // 光盘驱动器

Win32_BaseBoard                        // 主板

Win32_BIOS                             // BIOS 芯片

Win32_ParallelPort                     // 并口

Win32_SerialPort                       // 串口

Win32_SoundDevice                      // 多媒体设置

Win32_USBController                    // USB 控制器

Win32_NetworkAdapter                   // 网络适配器

Win32_NetworkAdapterConfiguration      // 网络适配器设置

Win32_Printer                          // 打印机

Win32_PrinterConfiguration             // 打印机设置

Win32_PrintJob                         // 打印机任务

Win32_TCPIPPrinterPort                 // 打印机端口

Win32_POTSModem                        // MODEM

Win32_POTSModemToSerialPort            // MODEM 端口

Win32_DesktopMonitor                   // 显示器

Win32_VideoController                  // 显卡细节。

Win32_VideoSettings                    // 显卡支持的显示模式。

Win32_TimeZone                         // 时区

Win32_SystemDriver                     // 驱动程序

Win32_DiskPartition                    // 磁盘分区

Win32_LogicalDisk                      // 逻辑磁盘

Win32_LogicalMemoryConfiguration       // 逻辑内存配置

Win32_PageFile                         // 系统页文件信息

Win32_PageFileSetting                  // 页文件设置

Win32_BootConfiguration                // 系统启动配置

Win32_OperatingSystem                  // 操作系统信息

Win32_StartupCommand                   // 系统自动启动程序

Win32_Service                          // 系统安装的服务

Win32_Group                            // 系统管理组

Win32_GroupUser                        // 系统组帐号

Win32_UserAccount                      // 用户帐号

Win32_Process                          // 系统进程

Win32_Thread                           // 系统线程

Win32_Share                            // 共享

Win32_NetworkClient                    // 已安装的网络客户端

Win32_NetworkProtocol                  // 已安装的网络协议
*/

function getSystemInfo() {
    var locator = new ActiveXObject("WbemScripting.SWbemLocator");
    var service = locator.ConnectServer(".");
    //CPU信息 
    var cpu = new Enumerator(service.ExecQuery("SELECT * FROM Win32_Processor")).item();
    var cpuType = cpu.Name, hostName = cpu.SystemName;
    //内存信息 
    var memory = new Enumerator(service.ExecQuery("SELECT * FROM Win32_PhysicalMemory"));
    for (var mem = [], i = 0; !memory.atEnd() ; memory.moveNext()) mem[i++] = { cap: memory.item().Capacity / 1024 / 1024, speed: memory.item().Speed }
    //系统信息 
    var system = new Enumerator(service.ExecQuery("SELECT * FROM Win32_ComputerSystem")).item();
    var physicMenCap = Math.ceil(system.TotalPhysicalMemory / 1024 / 1024), curUser = system.UserName, cpuCount = system.NumberOfProcessors

    return { cpuType: cpuType, cpuCount: cpuCount, hostName: hostName, curUser: curUser, memCap: physicMenCap, mem: mem }
}