using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class PlanForm
    {
        [Inject]
        public IPlanService PlansService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private PlanDetail _model = new PlanDetail();
        private bool _isBusy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                FormFile coverfile = new FormFile(_stream,_fileName);
                await PlansService.CreateAsync(_model, coverfile);
                _errorMessage = "Done";
                Navigation.NavigateTo("/plans");
            }
            catch (ApiException ex)
            {
                // Handle the errors of the API
                // TODO: Log those errors
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                // Handle Errors
                _errorMessage = ex.Message;
            }
            _isBusy = false;


        }

        private async Task OnChooseFileAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                if (file.Size > 2097152)
                {
                    _errorMessage = "The file must be equal or less than 2MB";
                    return;
                }

                string[] allowedExtenstions = new[] { ".jpg", ".png", ".bmp", ".svg" };

                string extension = Path.GetExtension(file.Name).ToLower();

                if (!allowedExtenstions.Contains(extension))
                {
                    _errorMessage = "Please choose a valid image file";
                    return;
                }

                using (var stream = file.OpenReadStream(2097152))
                {
                    var buffer = new byte[file.Size];
                    await stream.ReadAsync(buffer, 0, (int)file.Size);
                    _stream = new MemoryStream(buffer);
                    _stream.Position = 0;
                    _fileName = file.Name;
                }
            }
        }
    }
}
