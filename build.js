const fs = require('fs');
const path = require('path');
const { argv } = require('process');

const files = {
	static: [
		"TestFairy.cs",
		"Android/testfairy-android-sdk.aar",
		"iOS/TestFairyUnityWrapper.h",
		"iOS/TestFairyUnityWrapper.m",
		"iOS/TestFairy.h",
		"iOS/libTestFairy.a",
		"iOS/upload-dsym.sh",
		"iOS/strip-architectures.sh"
	],
	xcframework: [
		"TestFairy.cs",
		"Android/testfairy-android-sdk.aar",
		"iOS/TestFairyUnityWrapper.h",
		"iOS/TestFairyUnityWrapper.m",
		"iOS/TestFairy.xcframework/Info.plist",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/Info.plist",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/Headers/TestFairy.h",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/Modules/module.modulemap",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/strip-architectures.sh",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/TestFairy",
		"iOS/TestFairy.xcframework/ios-armv7_arm64/TestFairy.framework/upload-dsym.sh",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/Headers/TestFairy.h",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/Info.plist",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/Modules/module.modulemap",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/_CodeSignature/CodeResources",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/strip-architectures.sh",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/TestFairy",
		"iOS/TestFairy.xcframework/ios-i386_x86_64-simulator/TestFairy.framework/upload-dsym.sh",
	]
};

const prepare = (files, root) => {
	if (fs.existsSync(root)) {
		fs.rmdirSync(root, { recursive: true });
	}
	fs.mkdirSync(root);

	files.forEach(file => {
		const destination = path.join(root, file);
		const source = path.join("Files", file);
		const directory = path.dirname(file);
		const fileMeta = path.join("Meta", `${file}.meta`);
		const destinationDirectory = path.dirname(destination);

		if (!fs.existsSync(destinationDirectory)) {
			fs.mkdirSync(destinationDirectory, { recursive: true });
		}

		if (!fs.existsSync(source)) {
			throw `Required File ${source} was not found.`;
		}

		if (!fs.existsSync(fileMeta)) {
			throw `Required meta file ${fileMeta} was not found.`;
		}

		const directoryMeta = path.join("Meta", `${directory}.meta`);
		if (directory !== "." && !fs.existsSync(directoryMeta)) {
			throw `Required directory meta file ${directoryMeta} was not found.`;
		}

		fs.copyFileSync(source, destination);

		const destinationFileMeta = path.join(root, `${file}.meta`);
		if (!fs.existsSync(destinationFileMeta)) {
			fs.copyFileSync(fileMeta, destinationFileMeta);
		}

		const destinationDirectoryMeta = path.join(root, `${directory}.meta`);
		if (directory !== "." && !fs.existsSync(destinationDirectoryMeta)) {
			fs.copyFileSync(directoryMeta, destinationDirectoryMeta);
		}
	});
};

prepare(files[argv[2]], "Plugins");
