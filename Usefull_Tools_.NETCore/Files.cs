using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

namespace UniversalTCPClientLibrary
{
    public class Files : Shared.Files
    {
        public async Task<bool> saveFile(string extension, string content)
        {
            try
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("GPX format", new List<string>() { extension });
                savePicker.SuggestedFileName = "error";
                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    await FileIO.WriteTextAsync(file, content);
                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == FileUpdateStatus.Complete)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ex.Source = "file.saveFile";
                Shared.ErrorManager.printOut(ex);
            }

            return false;
        }

        public override async void choose(string text, string fileType)
        {
            try
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { fileType });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = "gps-track";

                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    await FileIO.WriteTextAsync(file, text);
                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == FileUpdateStatus.Complete)
                    {
                        // saved
                    }
                    else
                    {
                        // couldn't be saved
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ex.Source = "file.choose";
                ErrorManager.printOut(ex);
            }
        }

        public override void saveFile()
        {
            throw new NotImplementedException();
        }
    }
}
