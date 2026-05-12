$json = $input | ConvertFrom-Json
$fp = $json.tool_input.file_path
if ($fp -match '\.cs$') {
    csharpier format $fp
}
