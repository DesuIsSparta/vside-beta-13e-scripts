$DC::dcFolder = $dynamicContentMod;
$Net::DynamicContentURL = "http://winbuild/scripts/orion" @ "/" @ $DC::dcFolder;
$DC::SkinsFolderName = "skins";
$DC::RemoteSkinsFolder = $Net::DynamicContentURL @ "/" @ $DC::SkinsFolderName;
$DC::LocalSkinsFolder = $DC::dcFolder @ "/" @ $DC::SkinsFolderName;
$DC::MarqueeFolderName = "marquee";
$DC::RemoteMarqueeFolder = $Net::DynamicContentURL @ "/" @ $DC::MarqueeFolderName;
$DC::UploadScript = $Net::DynamicContentURL @ "/post_dump.php";
$DC::DownloadFolder = $Net::DynamicContentURL @ "/uploaded_files/files";
$DC::LocalAvatarFolder = $DC::dcFolder @ "/" @ "avatars";
$DC::GUIFolderName = $DC::dcFolder @ "/" @ "gui";
$DC::CacheFolderName = $DC::dcFolder @ "/" @ "cache";

