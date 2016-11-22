﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ACMESharp.Ext;
using ACMESharp.Vault.Model;
using ACMESharp.Vault.Util;
using ACMESharp.Util;

namespace ACMESharp.Vault.Providers
{

    [VaultProvider(PROVIDER_NAME,
            Label = "Local Disk Vault",
            Description = "Vault provider based on system-local folder and files.")]
    public class LocalDiskVaultProvider : IVaultProvider
    {
        public const string PROVIDER_NAME = "local";

        public static readonly ParameterDetail ROOT_PATH = new ParameterDetail(
                nameof(LocalDiskVault.RootPath), ParameterType.TEXT,
                isRequired: true, label: "Root Path",
                desc: "Specifies the directory path where vault data files will be rooted.");

        public static readonly ParameterDetail CREATE_PATH = new ParameterDetail(
                nameof(LocalDiskVault.CreatePath), ParameterType.BOOLEAN,
                isRequired: true, label: "Create Path",
                desc: "Specifies the Root Path should be created if it does not exist.");

        static readonly ParameterDetail[] PARAMS = { ROOT_PATH, CREATE_PATH, };

        public IEnumerable<ParameterDetail> DescribeParameters()
        {
            return PARAMS;
        }

        public IVault GetVault(IReadOnlyDictionary<string, object> initParams)
        {
            var vault = new LocalDiskVault();

            if (initParams.ContainsKey(ROOT_PATH.Name))
                vault.RootPath = initParams[ROOT_PATH.Name] as string;
            if (initParams.ContainsKey(CREATE_PATH.Name))
                vault.CreatePath = (initParams[CREATE_PATH.Name] as bool?).GetValueOrDefault();

            vault.Init();

            return vault;
        }

        public void Dispose()
        { }
    }
}
