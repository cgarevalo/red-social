function clearVideoPreview() {
    let fuVideo = document.getElementById(fuVideosPostClientId);
    let videoContainer = document.getElementById(vidContainerClientId);
    let videoSource = document.getElementById(vidPreviewClientId);
    let btnRemoveVideo = document.getElementById(btnRemoveVideoClientId);
    let btnPost = document.getElementById(btnPostClientId);

    // Limpiar el campo FileUpload
    fuVideo.value = "";

    // Vaciar el <source> y ocultar el contenedor del video  
    videoSource.src = "";
    videoContainer.style.display = 'none';
    btnRemoveVideo.style.display = 'none';

    //Deshabilita el boton de posteo
    btnPost.disabled = true;
}