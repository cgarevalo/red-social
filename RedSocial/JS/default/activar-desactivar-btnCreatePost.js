function checkInput() {
    // Obtine los elementos por su ClientID generado por ASP.NET
    let textBox = document.getElementById(txtPostClientId);
    let postButton = document.getElementById(btnPostClientId);
    let ddlTypePost = document.getElementById(ddlTypePostClientId);

    // Obtener los controles FileUpload por sus IDs
    let fileFotos = document.getElementById(fuFotosPostClientId);
    let fileVideos = document.getElementById(fuVideosPostClientId);

    // Variables para verificar si se seleccionó un archivo o hay texto
    let isTextSelected = textBox.value.trim() !== "";
    let isFileSelected = false;

    // Comprueba el tipo de publicación seleccionado
    if (ddlTypePost.value === "1") { // Subir texto
        // Habilita o deshabilita el botón según el contenido del TextBox
        postButton.disabled = !isTextSelected;
        return; // Salir de la función
    } else if (ddlTypePost.value === "2") { // Subir video
        isFileSelected = fileVideos.files.length > 0; // Si se seleccionó un archivo de video
        postButton.disabled = !isFileSelected;
    } else if (ddlTypePost.value === "3") { // Subir foto
        isFileSelected = fileFotos.files.length > 0; // Si se seleccionó un archivo de foto
        postButton.disabled = !isFileSelected;
    } else {
        postButton.disabled = true;
    }

    //// Habilita o deshabilita el botón según el contenido del TextBox
    //postButton.disabled = textBox.value.trim() === "";
}