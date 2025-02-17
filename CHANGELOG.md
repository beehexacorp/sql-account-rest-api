# Feb 17, 2025
## Update API POST app/update
### Overview
Improve windows service and IIS deployment method by get rid of hardcoding service PORT and deployment method
### Files changed
- Edit `src/SqlAccountRestAPI/Helpers/SqlAccountAppHelper.cs`
# Feb 14, 2025
## Improve SQLACC not response handler
### Overview
The previous version will restart SQLACC whenever `app` is not defined

This will interupt client task if they are interacting with SQLACC then the SqlAccountRestAPI receive a request 
### Files changed
- Edit `src/SqlAccountRestAPI/Core/SqlAccountFactory.cs` - Rearrange the `EndProcess` function calling
## Clean dump try-catch
### Overview
Some try-catch blocks are not necessary and may hinder the workflow 
### Files changed
- Edit `src/SqlAccountRestAPI/Core/SqlAccountFactory.cs`
- Edit `src/SqlAccountRestAPI/Helpers/SqlAccountAppHelper.cs`
# Jan 22, 2025
## Improve SQLACC shutdown by client case
### Overview
Add function `IsComObjectResponsive` to check if SQLACC has been closed by client or not

This is more effective and stable than the previous one
### Files changed
- Edit `Helpers/SystemHelper.cs` - Add function `IsComObjectResponsive`