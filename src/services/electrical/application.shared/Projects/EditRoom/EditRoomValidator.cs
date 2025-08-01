namespace TriPower.Electrical.Application.Shared.Projects.EditRoom;

public class EditRoomValidator : AbstractValidator<EditRoomRequest>
{
    public EditRoomValidator()
    {
        RuleFor(request => request.ProjectId)
            .NotEmpty();

        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(request => request.Perimeter)
            .NotEmpty();

        RuleFor(request => request.Area)
            .NotEmpty();

        RuleFor(request => request.Classification)
            .NotEmpty();
        RuleFor(request => request.Type)
            .NotEmpty();
    }
}