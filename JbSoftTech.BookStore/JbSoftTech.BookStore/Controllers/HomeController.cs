using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JbSoftTech.BookStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Programs_List
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
                GetInstalledPrograms();
            }

            private void action_btn_get_Click(object sender, EventArgs e)
            {
                string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
                {
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey sk = rk.OpenSubKey(skName))
                        {
                            try
                            {

                                var displayName = sk.GetValue("DisplayName");
                                var size = sk.GetValue("EstimatedSize");

                                ListViewItem item;
                                if (displayName != null)
                                {
                                    if (size != null)
                                        item = new ListViewItem(new string[] {displayName.ToString(),
                                                       size.ToString()});
                                    else
                                        item = new ListViewItem(new string[] { displayName.ToString() });
                                    lstDisplayHardware.Items.Add(item);
                                }
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                    label1.Text += " (" + lstDisplayHardware.Items.Count.ToString() + ")";
                }
            }

        }
        public class InstalledPrograms
        {
            public static List<string> GetInstalledPrograms()
            {
                var result = new List<string>();
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32));
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64));
                result.Sort();
                return result;
            }
            private static string cleanText(string dirtyText)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 .()+-]");
                string result = rgx.Replace(dirtyText, "");
                return result;
            }
            private static IEnumerable<string> GetInstalledProgramsFromRegistry(RegistryView registryView)
            {
                var result = new List<string>();
                List<string> uninstall = new List<string>();
                uninstall.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                uninstall.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (string registry_key in uninstall)
                {
                    using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView).OpenSubKey(registry_key))
                    {
                        foreach (string subkey_name in key.GetSubKeyNames())
                        {
                            using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                            {
                                if (IsProgramVisible(subkey))
                                {
                                    result.Add(cleanText(subkey.GetValue("DisplayName").ToString()).ToString());
                                }
                            }
                        }
                    }
                    using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView).OpenSubKey(registry_key))
                    {
                        if (key != null)
                        {
                            foreach (string subkey_name in key.GetSubKeyNames())
                            {
                                using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                                {
                                    if (IsProgramVisible(subkey))
                                    {
                                        result.Add(cleanText(subkey.GetValue("DisplayName").ToString()).ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }
    }


    //if (!([Diagnostics.Process]::GetCurrentProcess().Path -match '\\syswow64\\'))
    //{
    //$uninstallPath = "\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\"
    //$uninstallWow6432Path = "\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\"
    //@(
    //if (Test-Path "HKLM:$uninstallWow6432Path" ) { Get-ChildItem "HKLM:$uninstallWow6432Path"}
    //if (Test-Path "HKLM:$uninstallPath" ) { Get-ChildItem "HKLM:$uninstallPath" }
    //if (Test-Path "HKCU:$uninstallWow6432Path") { Get-ChildItem "HKCU:$uninstallWow6432Path"}
    //if (Test-Path "HKCU:$uninstallPath" ) { Get-ChildItem "HKCU:$uninstallPath" }
    //) |
    //ForEach-Object { Get-ItemProperty $_.PSPath } |
    //Where-Object {
    //$_.DisplayName -and !$_.SystemComponent -and !$_.ReleaseType -and !$_.ParentKeyName -and($_.UninstallString -or $_.NoRemove)
    //} |
    //Sort-Object DisplayName |
    //Select-Object DisplayName
    //}
    //else
    //{
    //"You are running 32-bit Powershell on 64-bit system. Please run 64-bit Powershell instead." | Write-Host -ForegroundColor Red
    //}
}
