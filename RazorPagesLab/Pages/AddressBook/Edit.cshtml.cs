using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		// Get entry from repo and check if it exists
		var byId = new EntryByIdSpecification(id);
		var entryList = _repo.Find(byId);

		if (entryList.Count == 0) 
		{
			RedirectToPage("/Error");
			return;
		}

		AddressBookEntry entry = entryList[0];

		// Set fields
		UpdateAddressRequest = new UpdateAddressRequest {
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
			var response = await _mediator.Send(UpdateAddressRequest);
			if (response == Guid.Empty) {
				return RedirectToPage("/Error");
			}
			return RedirectToPage("Index");
		}
		return Page();
	}
}