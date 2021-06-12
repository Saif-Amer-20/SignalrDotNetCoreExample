using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalMarketsCore.DataModel;
using Microsoft.AspNetCore.Authorization;
using GlobalMarketsCore.Services;
using GlobalMarketsCore.Events;
using static GlobalMarketsCore.Events.EventDelegates;
using Microsoft.AspNetCore.SignalR;
using GlobalMarketsCore.Hubs;

namespace GlobalMarketsCore.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IHubContext<MessageHub> _hubContext;

        public ClientsController(IClientService clientService,  IHubContext<MessageHub> hubContext)
        {
            _clientService = clientService;
            _hubContext = hubContext;
        }

        // GET: Clients
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetClients(int? page, int? limit, string sortBy, string direction, string firstName = null, string lastName = null, int groupId = 0,int statusId = 0, string email = null)
        {
            int total;
            var owner = User.Identity.Name;
            var records = _clientService.GetClientsJsonData(page, limit, sortBy, direction, out total, firstName, lastName, groupId, statusId, email, owner);

            var result = Json(new { records, total });

            return result;
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.Details(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Gender,DateOfBirth,Age")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(client.Email))
                {
                    var emailExists = await _clientService.GetClient(client.Email);
                    if (emailExists != null)
                    {
                        ModelState.AddModelError("Email","Email is already registered before.");
                        return View(client);
                    }
                }


                await _clientService.Create(client);
                await _hubContext.Clients.All.clientListener($"welcome to { client.FirstName} as added successfully </hr>");
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterClient(string firstName, int categoryId, string lastName = null, string mobile = null, string email = null, string gender = null)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Json("First Name is required.");
            }
            else if (categoryId == 0)
            {
                return Json("Category is required.");
            }

            if (!string.IsNullOrEmpty(email))
            {
                var emailExists = await _clientService.GetClient(email);
                if (emailExists != null)
                {
                    return Json("Email is already registered before.");
                }
            }

            var clientExists = await _clientService.GetClient(firstName, categoryId);
            if (clientExists != null)
            {
                return Json("This client is already registered before.");
            }

            var client = new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                Mobile = mobile,
                Email = email,
                Gender = gender

            };
            await _clientService.Create(client);

          //  await _hubContext.Clients.All.clientListener("send", "Hello from the hub server at " +DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            await _hubContext.Clients.All.clientListener($"welcome to { client.FirstName} </hr>");


            return Json("Success");
        }


        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.Details(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Gender,DateOfBirth,Age")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var clientToBeEdit = await _clientService.Details(id);
                    if (clientToBeEdit.Email.Trim().ToLower() != client.Email)
                    {
                        var emailExists = await _clientService.GetClient(client.Email);
                        if (emailExists != null)
                        {
                            ModelState.AddModelError("Email", "Email is already registered before.");
                            return View(client);
                        }
                    }

                    await _clientService.Edit(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _clientService.ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await _hubContext.Clients.All.clientListener($"welcome to { client.FirstName} as edited successfully </hr>");
                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.Details(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


       
    }
}
