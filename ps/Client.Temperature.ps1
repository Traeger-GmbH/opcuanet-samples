# Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Write-Host ""
Write-Host "Welcome to 'Client.Temperature' sample for Opc.UaFx.Client!" -ForegroundColor Green
Write-Host "Copyright (c) Traeger Industry Components GmbH. All Rights Reserved." -ForegroundColor DarkGray
Write-Host ""
. .\Opc.UaFx.Client.Bootstrapper.ps1
#. .\Opc.UaFx.Client.Bootstrapper.ps1 -Silent
Invoke-Bootstrap

Write-Host "Starting sample..." -ForegroundColor Yellow
$client = New-Object -TypeName Opc.UaFx.Client.OpcClient -ArgumentList "opc.tcp://localhost:4840" 

try {
    $client.Connect()
    
    while ($true) {
        $temperature = $client.ReadNode("ns=2;s=Temperature");
        Write-Host "Current Temperature is $temperature degree celsius" -ForegroundColor DarkGray

        Start-Sleep 1
    }
}
catch {
    Write-Error "$($_.Exception.GetType().FullName): $($_.Exception.Message)"
}
finally {
    $client.Disconnect()
    Write-Host "Disconnected!" -ForegroundColor DarkGray
}
