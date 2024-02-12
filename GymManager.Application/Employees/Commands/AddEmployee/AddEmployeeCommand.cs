﻿using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace GymManager.Application.Employees.Commands.AddEmployee;

public class AddEmployeeCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'Adres e-mail' jest wymagane.")]
    [DisplayName("Adres e-mail")]
    [EmailAddress(ErrorMessage = "Pole 'Adres e-mail' nie jest prawidłowym adresem e-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
    [DisplayName("Hasło")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Pole 'Potwierdź hasło' jest wymagane.")]
    [DisplayName("Potwierdź hasło")]
    [Compare("Password", ErrorMessage = "Pola 'Hasło' i 'Potwierdź hasło' są różne.")]
    public string ConfirmPassowrd { get; set; }

    [Required(ErrorMessage = "Pole 'Imię' jest wymagane.")]
    [DisplayName("Imię")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane.")]
    [DisplayName("Nazwisko")]
    public string LastName { get; set; }


    [Required(ErrorMessage = "Pole 'Kraj' jest wymagane.")]
    [DisplayName("Kraj")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Pole 'Miejscowość' jest wymagane.")]
    [DisplayName("Miejscowość")]
    public string City { get; set; }

    [Required(ErrorMessage = "Pole 'Ulica' jest wymagane.")]
    [DisplayName("Ulica")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Pole 'Numer domu' jest wymagane.")]
    [DisplayName("Numer domu")]
    public string StreetNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Kod pocztowy' jest wymagane.")]
    [DisplayName("Kod pocztowy")]
    public string ZipCode { get; set; }



    [Required(ErrorMessage = "Pole 'Data zatrudnienia' jest wymagane.")]
    [DisplayName("Data zatrudnienia")]
    public DateTime DateOfEmployment { get; set; }

    [DisplayName("Data zwolnienia")]
    public DateTime? DateOfDismissal{ get; set; }

    [Required(ErrorMessage = "Pole 'Wynagrodzenie' jest wymagane.")]
    [DisplayName("Wynagrodzenie")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Pole 'Stanowisko' jest wymagane.")]
    [DisplayName("Stanowisko")]
    public int PositionId { get; set; }

    [DisplayName("Zdjęcie profilowe")]
    public string ImageUrl { get; set; }

    [DisplayName("Adres URL strony profilowej")]
    public string WebsiteUrl { get; set; }

    [DisplayName("Strona profilowa")]
    public string WebsiteRaw { get; set; }
}
