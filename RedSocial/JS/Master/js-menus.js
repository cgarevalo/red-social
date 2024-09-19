document.addEventListener('DOMContentLoaded', function () {
    // Obtiene la imagen del perfil y el menú desplegable
    const profileImage = document.getElementById(imgProfileNavClienId);
    const profileMenu = document.getElementById(profileMenuClientId);

    // Maneja el clic en la imagen del perfil
    profileImage.addEventListener('click', function () {
        // Alterna la visibilidad del menú desplegable
        profileMenu.style.display = profileMenu.style.display === 'none' || profileMenu.style.display === '' ? 'block' : 'none';
    });

    // Oculta el menú desplegable cuando se hace clic en cualquier otro lugar
    document.addEventListener('click', function (event) {
        if (!profileImage.contains(event.target) && !profileMenu.contains(event.target)) {
            profileMenu.style.display = 'none';
        }
    });
});