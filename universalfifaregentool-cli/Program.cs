using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FifaLibrary;

namespace universalfifaregentool_cli
{
    public class Program
    {
        private static FifaFat m_FatFile;

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;

            tools tools = new tools();

            cli cli = new cli();

            string gameDir = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--gamedir" && i < args.Length - 1)
                {
                    gameDir = args[i + 1];
                }
            }

            if (gameDir != null)
            {
                cli.ShowBanner();
                Console.WriteLine($"[!] Game directory set to: {gameDir}");
                tools.execRegen(gameDir);
            }
            else
            {
                cli.ShowBanner();
                cli.ShowHelpPage();
            }
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";
            if (!assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture))
            {
                path = $"{assemblyName.CultureInfo}\\${path}";
            }

            System.IO.Stream stream1 = executingAssembly.GetManifestResourceStream(path);
            var stream = stream1;
            if (stream == null)
                return null;

            var assemblyRawBytes = new byte[stream.Length];
            stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
            return Assembly.Load(assemblyRawBytes);
        }

        class cli
        {
            public static void ShowHelpPage()
            {
                Console.WriteLine("Usage: regen-cli.exe --gamedir 'directory'");
                Console.WriteLine("Parameters:");
                Console.WriteLine("--gamedir    Specifies the game directory.");
                Console.WriteLine("              'directory' should be replaced with the actual directory path.");
            }

            public static void ShowBanner()
            {
                Console.WriteLine(@"
██╗░░░██╗███╗░░██╗██╗██╗░░░██╗███████╗██████╗░░██████╗░█████╗░██╗░░░░░  ███████╗██╗███████╗░█████╗░
██║░░░██║████╗░██║██║██║░░░██║██╔════╝██╔══██╗██╔════╝██╔══██╗██║░░░░░  ██╔════╝██║██╔════╝██╔══██╗
██║░░░██║██╔██╗██║██║╚██╗░██╔╝█████╗░░██████╔╝╚█████╗░███████║██║░░░░░  █████╗░░██║█████╗░░███████║
██║░░░██║██║╚████║██║░╚████╔╝░██╔══╝░░██╔══██╗░╚═══██╗██╔══██║██║░░░░░  ██╔══╝░░██║██╔══╝░░██╔══██║
╚██████╔╝██║░╚███║██║░░╚██╔╝░░███████╗██║░░██║██████╔╝██║░░██║███████╗  ██║░░░░░██║██║░░░░░██║░░██║
░╚═════╝░╚═╝░░╚══╝╚═╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═════╝░╚═╝░░╚═╝╚══════╝  ╚═╝░░░░░╚═╝╚═╝░░░░░╚═╝░░╚═╝

██████╗░███████╗░██████╗░███████╗███╗░░██╗███████╗██████╗░░█████╗░████████╗░█████╗░██████╗░
██╔══██╗██╔════╝██╔════╝░██╔════╝████╗░██║██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗██╔══██╗
██████╔╝█████╗░░██║░░██╗░█████╗░░██╔██╗██║█████╗░░██████╔╝███████║░░░██║░░░██║░░██║██████╔╝
██╔══██╗██╔══╝░░██║░░╚██╗██╔══╝░░██║╚████║██╔══╝░░██╔══██╗██╔══██║░░░██║░░░██║░░██║██╔══██╗
██║░░██║███████╗╚██████╔╝███████╗██║░╚███║███████╗██║░░██║██║░░██║░░░██║░░░╚█████╔╝██║░░██║
╚═╝░░╚═╝╚══════╝░╚═════╝░╚══════╝╚═╝░░╚══╝╚══════╝╚═╝░░╚═╝╚═╝░░╚═╝░░░╚═╝░░░░╚════╝░╚═╝░░╚═╝
");
            }
        }

        class tools
        {
            public static void execRegen(string gameDir)
            {
                if (gameDir.Length == 0)
                {
                    Console.WriteLine("[!] Enter a valid path before continuing!");
                }
                else
                {
                    CreateFat(gameDir);
                    RegenerateFatBh();
                }
            }
            private static void CreateFat(string rootPath)
            {
                Console.WriteLine("[*] Creating FatFile...");
                m_FatFile = FifaFat.Create(rootPath);
                Console.WriteLine("[*] Created FatFile successfully!");
            }
            private static void RegenerateFatBh()
            {
                if (m_FatFile != null)
                {
                    Console.WriteLine("[*] Regeneratring BH...");
                    m_FatFile.RegenerateAllBh(true);
                    Console.WriteLine("[*] Done!");
                }
            }
        }
    }
}
