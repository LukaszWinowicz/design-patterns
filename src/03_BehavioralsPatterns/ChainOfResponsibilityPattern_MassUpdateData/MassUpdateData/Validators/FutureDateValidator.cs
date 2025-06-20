﻿using MassUpdateData.Handlers;
using MassUpdateData.Models;

namespace MassUpdateData.Validators;

public class FutureDateValidator : ValidationHandler
{
    private readonly Func<MassUpdateDto, DateTime> _dateProvider;
    private readonly string _fieldName;

    public FutureDateValidator(Func<MassUpdateDto, DateTime> dateProvider, string fieldName)
    {
        _dateProvider = dateProvider;
        _fieldName = fieldName;
    }

    public override void Validate(ValidationRequest request)
    {
        // Używamy "sposobu", który dostaliśmy, aby pobrać datę
        var dateToValidate = _dateProvider(request.Dto);

        // Używamy obecnej daty z uwzględnieniem strefy czasowej
        var today = DateTime.Today;

        if (dateToValidate.Date < today)
        {
            // Używamy nazwy pola do zbudowania komunikatu
            request.ValidationErrors.Add($"{_fieldName} must be today or a future date.");
        }

        PassToNext(request);
    }
}
