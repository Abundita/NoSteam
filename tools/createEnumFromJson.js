var enm = {
    "name": "EAppUpdateError",
    "items":
        [
            ["NoError", "0"],
            ["UnspecifiedError", "1"],
            ["Paused", "2"],
            ["Canceled", "3"],
            ["Suspended", "4"],
            ["NoSubscription", "5"],
            ["NoConnection", "6"],
            ["ConnectionTimeout", "7"],
            ["MissingDecryptionKey", "8"],
            ["MissingConfiguration", "9"],
            ["DiskReadFailure", "10"],
            ["DiskWriteFailure", "11"],
            ["NotEnoughDiskSpace", "12"],
            ["CorruptGameFiles", "13"],
            ["WaitingForDisk", "14"],
            ["InvalidInstallPath", "15"],
            ["ApplicationRunning", "16"],
            ["DependencyFailure", "17"],
            ["NotInstalled", "18"],
            ["UpdateRequired", "19"],
            ["StillBusy", "20"],
            ["NoConnectionToContentServers", "21"],
            ["InvalidApplicationConfiguration", "22"],
            ["InvalidContentConfiguration", "23"],
            ["ManifestUnavailable", "24"],
            ["NotReleased", "25"],
            ["RegionRestricted", "26"],
            ["CorruptDepotCache", "27"],
            ["MissingExecutable", "28"],
            ["InvalidPlatform", "29"],
            ["InvalidFileSystem", "30"],
            ["CorruptUpdateFiles", "31"],
            ["DownloadDisabled", "32"],
            ["SharedLibraryLocked", "33"],
            ["PurchasePending", "34"],
            ["OtherSessionPlaying", "35"],
            ["CorruptDownload", "36"],
            ["CorruptDisk", "37"],
            ["MissingFilePermissions", "38"],
            ["FileLocked", "39"],
            ["ContentUnavailable", "40"],
            ["Requires64bitOperatingSystem", "41"],
            ["MissingUpdateFiles", "42"],
            ["NotEnoughDiskQuota", "43"],
            ["SiteLicenseLocked", "44"],
            ["ParentalControlBlocked", "45"],
            ["CreateProcessFailed", "46"],
            ["SteamClientOutOfDate", "47"],
            ["AllowedPlaytimeExceeded", "48"],
            ["CorruptFileSignature", "49"],
            ["MissingGameFiles", "50"],
            ["CompatToolFailed", "51"],
            ["InstallPathRemoved", "52"],
            ["InvalidBackupPath", "53"],
            ["InvalidPasscode", "54"],
            ["SelfUdpating", "55"]
        ]
}


console.log("namespace OpenSteamworks.Enums;");
console.log("");
console.log("public enum " + enm.name + " : System.UInt32");
console.log("{");
enm.items.forEach((cb)=> {
	console.log(`       k_${enm.name+cb[0]} = ${cb[1]},`);
})
console.log("};");
