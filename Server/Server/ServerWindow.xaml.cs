﻿using Server.ScreenGrabber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Server.WindowsUtils;
using Server.Network;
using Server.Properties;
using Server.ProtoGeneration;
using System.Text.RegularExpressions;
using MahApps.Metro.Controls;
using System.Runtime.ExceptionServices;
using System.Security;

namespace Server
{
    
    public partial class ServerWindow : MetroWindow
    {
        private LdpServer server;
        private LdpLabelStatus labelStatus;
        private bool OS_SUPPORTED = false;
        public ServerWindow()
        {
            
            InitializeComponent();
            server = LdpServer.GetInstance();
            labelStatus = LdpLabelStatus.GetInstance();
            lblConnectionStatus.DataContext = labelStatus;
            OS_SUPPORTED = LdpUtils.CheckStartupWindowsVersion(this);
            if (OS_SUPPORTED)
                StartServer();
        }

        private void StartServer()
        {
            if (server != null)
                server.Start();
        }

        private void StopServer()
        {
            if (server != null)
            {
                server.DisconnectClient();
                server.Stop();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (OS_SUPPORTED)
            {
                if (MessageBox.Show("Close server?", "Question",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    StopServer();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
                e.Cancel = false;
            base.OnClosing(e);

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void btnSetPassword_Click(object sender, RoutedEventArgs e)
        {
            Password_form password_form = new Password_form();
            password_form.ShowDialog();
        }

        private void btnFile_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
