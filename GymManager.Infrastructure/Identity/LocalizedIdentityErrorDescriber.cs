using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Infrastructure.Identity;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    private const string Password = "Password";
    private const string Email = "Email";

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło i potwierdzone hasło są różne."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło musi zawierać przynajmniej 1 cyfrę."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło musi zawierać przynajmniej 1 małą literę."
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło musi zawierać przynajmniej 1 znak specjalny."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło musi zawierać przynajmniej 1 wielką literę."
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = Password,
            Description = "Hasło musi mieć przynjamniej 8 i nie więcej niż 100 znaków."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = Email,
            Description = "Wybrany adres email jest już zajęty."
        };
    }
}
