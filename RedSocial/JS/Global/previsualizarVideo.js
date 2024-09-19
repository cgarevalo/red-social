function previewVideo(fileUploadId, videoContainerId, videoSourceId) {
    let fileInput = document.getElementById(fileUploadId);
    let videoContainer = document.getElementById(videoContainerId);
    let videoSource = document.getElementById(videoSourceId);
    let btnRemoveVideo = document.getElementById(btnRemoveVideoClientId);

    if (fileInput.files && fileInput.files[0]) {
        let file = fileInput.files[0];

        // Asegúrate de que el archivo seleccionado sea un video
        if (file.type.startsWith('video')) {
            let videoURL = URL.createObjectURL(file);
            videoSource.src = videoURL;

            // Mostrar el contenedor del video y actualizar el elemento <source>
            videoContainer.style.display = 'block';
            btnRemoveVideo.style.display = 'block';
            videoSource.parentElement.load(); // Cargar el nuevo video
        } else {
            btnRemoveVideo.style.display = 'none';
            alert('Por favor, selecciona un archivo de video válido.');
        }
    } else {
        // Si no hay archivo seleccionado (se cancela la selección), oculta el video
        clearVideoPreview();
    }
}