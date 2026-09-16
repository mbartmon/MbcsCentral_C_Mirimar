Param([string]$domain,[string]$server,[string]$fullname,[string]$givenname,[string]$surname,[string]$accountname,[string]$password,[string]$description)
New-ADUser -Description:$description -DisplayName:$fullname -GivenName:$givenname -Name:$fullname -Path:"CN=Users,DC=$domain,DC=com" -SamAccountName:$accountname -Server:$server -Surname:$surname -Type:"User" -UserPrincipalName:$accountname"@"$domain".com"
Set-ADAccountPassword -Identity:"CN=$fullname,CN=Users,DC=$domain,DC=com" -NewPassword:(ConvertTo-SecureString -AsPlainText $password -Force) -Reset:$true -Server:$server
Enable-ADAccount -Identity:"CN=$fullname,CN=Users,DC=$domain,DC=com" -Server:$server
Get-ADObject -Identity:"CN=$fullname,CN=Users,DC=$domain,DC=com" -IncludeDeletedObjects:$false -Properties:primaryGroupID,memberOf,objectSid -Server:$server
Add-ADPrincipalGroupMembership -Identity: "CN=$fullname,CN=Users,DC=$domain,DC=com" -MemberOf $description -Server $server
Set-ADAccountControl -AccountNotDelegated:$false -AllowReversiblePasswordEncryption:$false -CannotChangePassword:$false -DoesNotRequirePreAuth:$false -Identity:"CN=$fullname,CN=Users,DC=$domain,DC=com" -PasswordNeverExpires:$true -Server:$server -UseDESKeyOnly:$false
Set-ADUser -ChangePasswordAtLogon:$false -Identity:"CN=$fullname,CN=Users,DC=$domain,DC=com" -Server:$server -SmartcardLogonRequired:$false
