﻿Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ProjectOnline\client\*.dll" -Destination "E:\ProjectOnline\resources\client-core"
Remove-Item "E:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll"
Remove-Item "E:\ProjectOnline\resources\client-core\*.pdb"
Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ClassicModules\Server\*.dll" -Destination "E:\ProjectOnline\resources\server-core"
Remove-Item "E:\ProjectOnline\resources\server-core\CitizenFX.Core.*.dll"
Remove-Item "E:\ProjectOnline\resources\server-core\*.pdb"