using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IRepo<AddressBookEntry> _repo;

	public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
        _mediator = mediator;
	}

	[BindProperty]
	public DeleteAddressRequest DeleteAddressRequest { get; set; }

	public void OnGet(Guid id)
    {
        // Get entry from repo and check if it exists (to display to user)
		var byId = new EntryByIdSpecification(id);
		var entryList = _repo.Find(byId);

		if (entryList.Count == 0) 
		{
			RedirectToPage("/Error");
			return;
		}

        AddressBookEntry entry = entryList[0];

		// Set fields
		DeleteAddressRequest = new DeleteAddressRequest {
			Id = entry.Id,
            Line1 = entry.Line1,
            Line2 = entry.Line2,
            City = entry.City,
            State = entry.State,
            PostalCode = entry.PostalCode
		};
    }


    public async Task<ActionResult> OnPost()
    {
        if (ModelState.IsValid)
		{
			_ = await _mediator.Send(DeleteAddressRequest);
			return RedirectToPage("Index");
		}

        return Page();
    }
}