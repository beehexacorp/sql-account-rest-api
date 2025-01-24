# Changelog

## [0.0.43] - 2025-01-22
### Added
- Added `Helpers/SystemHelper.cs` with the function `IsComObjectResponsive` to check if SQL Account (SQLACC) has been closed by the user or not.
- Added release to the logout function. This function behaves the same as when the user closes the application.

### Changed
- Cleaned the "Add Customer Payment" request by removing the unnecessary `CODE` field.
- Moved the function `EndProcess` from `Core/SqlAccountFactory.cs` to `Helpers/SystemHelper.cs`.

### Fixed
- Fixed the API related to `GET FROM_DAYS_AGO` where time calculation now starts from the beginning of the day.
- Fixed the relogin feature to work correctly when the credential file is deleted.

---

## [0.0.42] - 2025-01-17
### Added
- Added a check to verify if SQLACC is logged in; if not, the system will perform a relogin.
- Added the parameter `autoLogin = False` for the API Login function.

---

## [0.0.41] - 2025-01-10
### Changed
- Updated the API to ensure compatibility with Windows startup deployment.
