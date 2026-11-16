$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing
$out = Join-Path $PSScriptRoot 'Assets'
New-Item -ItemType Directory -Force $out | Out-Null
function Draw-Icon($name, $w, $h) {
  $bmp = [System.Drawing.Bitmap]::new($w,$h)
  $g = [System.Drawing.Graphics]::FromImage($bmp)
  $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
  $g.Clear([System.Drawing.Color]::FromArgb(16,16,18))
  $s = [Math]::Min($w,$h)
  $pen = [System.Drawing.Pen]::new([System.Drawing.Color]::FromArgb(218,55,63), [float]($s*0.06))
  $brush = [System.Drawing.SolidBrush]::new([System.Drawing.Color]::White)
  $x = $w/2; $y = $h/2
  $g.DrawRectangle($pen,[float]($x-$s*.34),[float]($y-$s*.25),[float]($s*.68),[float]($s*.5))
  $points = [System.Drawing.PointF[]]@(
    ([System.Drawing.PointF]::new([float]($x-$s*.11),[float]($y-$s*.15))),
    ([System.Drawing.PointF]::new([float]($x+$s*.16),[float]$y)),
    ([System.Drawing.PointF]::new([float]($x-$s*.11),[float]($y+$s*.15))))
  $g.FillPolygon($brush,$points)
  $bmp.Save((Join-Path $out $name),[System.Drawing.Imaging.ImageFormat]::Png)
  $brush.Dispose(); $pen.Dispose(); $g.Dispose(); $bmp.Dispose()
}
Draw-Icon 'StoreLogo.png' 50 50
Draw-Icon 'Square150x150Logo.png' 150 150
Draw-Icon 'Square44x44Logo.png' 44 44
Draw-Icon 'SplashScreen.png' 620 300
