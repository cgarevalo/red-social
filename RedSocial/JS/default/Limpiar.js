function clearImagePreview() {
    let imgElement = document.getElementById(imgFotoPostClientId);
    let btnRemoveImage = document.getElementById(btnRemoveImageClientId);
    let fileUpload = document.getElementById(fuFotosPostClientId);
    let btnPost = document.getElementById(btnPostClientId);

    // Vaciar la imagen de previsualización
    imgElement.src = "";

    // Ocultar el botón de cierre "X"
    btnRemoveImage.style.display = "none";

    // Vaciar el contenido del control FileUpload
    fileUpload.value = "";

    //Deshabilita el boton de posteo
    btnPost.disabled = true;
}
