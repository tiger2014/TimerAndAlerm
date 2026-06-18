#!/usr/bin/env bash
# 把 TimerAndAlerm 打包成 macOS 的 .app 包。
# 用法: ./package-mac.sh [arm64|x64]   默认跟随当前机器架构。
set -euo pipefail

cd "$(dirname "$0")"

# ── 配置 ──
APP_NAME="Timer and Alarm"          # Finder 里显示的名字（.app 文件名）
EXE_NAME="TimerAndAlerm"            # 发布出来的可执行文件名（= csproj 程序集名）
BUNDLE_ID="com.tiger2014.timerandalerm"
VERSION="1.0.0"
ICON_SRC="Asset/bell.png"          # 512x512 源图，用来生成 .icns

# ── 选架构 / RID ──
HOST_ARCH="$(uname -m)"            # arm64 或 x86_64
ARCH="${1:-$HOST_ARCH}"
case "$ARCH" in
  arm64)          RID="osx-arm64" ;;
  x64|x86_64)     RID="osx-x64" ;;
  *) echo "未知架构: $ARCH（用 arm64 或 x64）"; exit 1 ;;
esac

OUT_DIR="dist"
APP_DIR="$OUT_DIR/$APP_NAME.app"
PUBLISH_DIR="bin/publish-$RID"

echo "==> 1/4 发布自包含二进制 ($RID)"
rm -rf "$PUBLISH_DIR"
dotnet publish TimerAndAlerm.csproj \
  -c Release \
  -r "$RID" \
  --self-contained true \
  -p:PublishSingleFile=false \
  -o "$PUBLISH_DIR"

echo "==> 2/4 搭 .app 目录结构"
rm -rf "$APP_DIR"
mkdir -p "$APP_DIR/Contents/MacOS"
mkdir -p "$APP_DIR/Contents/Resources"

# 发布产物（exe + 所有 dll + 原生库 + Asset 里的 mp3）整个塞进 MacOS/
cp -R "$PUBLISH_DIR/." "$APP_DIR/Contents/MacOS/"
chmod +x "$APP_DIR/Contents/MacOS/$EXE_NAME"

echo "==> 3/4 由 $ICON_SRC 生成 AppIcon.icns"
ICONSET="$(mktemp -d)/AppIcon.iconset"
mkdir -p "$ICONSET"
for size in 16 32 128 256 512; do
  sips -z "$size" "$size"       "$ICON_SRC" --out "$ICONSET/icon_${size}x${size}.png"   >/dev/null
  dbl=$((size * 2))
  sips -z "$dbl"  "$dbl"        "$ICON_SRC" --out "$ICONSET/icon_${size}x${size}@2x.png" >/dev/null
done
iconutil -c icns "$ICONSET" -o "$APP_DIR/Contents/Resources/AppIcon.icns"

echo "==> 4/4 写 Info.plist"
cat > "$APP_DIR/Contents/Info.plist" <<PLIST
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleName</key>            <string>$APP_NAME</string>
    <key>CFBundleDisplayName</key>     <string>$APP_NAME</string>
    <key>CFBundleExecutable</key>      <string>$EXE_NAME</string>
    <key>CFBundleIdentifier</key>      <string>$BUNDLE_ID</string>
    <key>CFBundleIconFile</key>        <string>AppIcon</string>
    <key>CFBundlePackageType</key>     <string>APPL</string>
    <key>CFBundleShortVersionString</key> <string>$VERSION</string>
    <key>CFBundleVersion</key>         <string>$VERSION</string>
    <key>LSMinimumSystemVersion</key>  <string>11.0</string>
    <key>NSHighResolutionCapable</key> <true/>
    <key>LSApplicationCategoryType</key> <string>public.app-category.utilities</string>
</dict>
</plist>
PLIST

# 去掉 Gatekeeper 的隔离属性，省得本机首次打开被拦
xattr -cr "$APP_DIR" 2>/dev/null || true

echo ""
echo "✅ 打包完成: $APP_DIR"
echo "   打开: open \"$APP_DIR\""
echo "   装入应用程序: cp -R \"$APP_DIR\" /Applications/"
