using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteModel : PageModel
{
    


    public async Task<ActionResult> OnPost()
    {


        return RedirectToPage("Index");
    }
}