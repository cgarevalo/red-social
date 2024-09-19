function toggleFileUpload() {
    // Obtener el valor seleccionado del DropDownList
    let ddl = document.getElementById(ddlTypePostClientId);
    let selectedValue = ddl.value;

    // Obtiene los divs de los controles FileUpload
    let divFotos = document.getElementById(divFuFotosClientId);
    let divVideos = document.getElementById(divFuVideosClientId);

    // Resetea los controles FileUpload
    resetMediaUploads();

    // Mostrar/Ocultar controles de acuerdo al valor seleccionado
    if (selectedValue === "2") {
        divFotos.style.display = "none";
        divVideos.style.display = "block";
    } else if (selectedValue === "3") {
        divFotos.style.display = "block";
        divVideos.style.display = "none";
    } else {
        divFotos.style.display = "none";
        divVideos.style.display = "none";
    }
}

function resetMediaUploads() {

    let fileFotos = document.getElementById(fuFotosPostClientId);
    let fileVideos = document.getElementById(fuVideosPostClientId);

    // Id del control de la imagen
    let imgFotoPost = document.getElementById(imgFotoPostClientId);

    let vidVideoPost = document.getElementById(vidPreviewClientId);

    // Resetea el control FileUpload para fotos
    if (fileFotos) {
        clearImagePreview(); // Llama a la función clearImagePreview() para limpiar la imagen
    }

    // Resetea el control FileUpload para videos
    if (fileVideos) {
        clearVideoPreview(); // Llama a la función clearVideoPreview() para limpiar el video       
    }

    // Vaciar el contenido de la imagen de previsualización
    if (imgFotoPost) {
        imgFotoPost.src = ""; // Vaciar la imagen de previsualización
    }

    // Llama a checkInput() para actualizar el estado del botón de posteo
    checkInput()
}