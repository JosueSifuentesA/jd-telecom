$(document).ready(()=>{
    const dropzoneContainer = document.querySelector(".dropzoneContainer");
    const fileInput = document.getElementById("fileInput");
    const image = document.getElementById("fileImg")
    const imageMessage = document.getElementById("fileMessageContainer")
    if($(image).attr("src") === undefined ){
        image.style.display = "none"
        imageMessage.style.display = "flex"
    }else{
        image.style.display = "block"
        imageMessage.style.display = "none"
    }

    dropzoneContainer.addEventListener("dragover", function (e) {
    e.preventDefault();
    e.stopPropagation();
    $(dropzoneContainer).addClass("dragover");
    });

    dropzoneContainer.addEventListener("dragleave", function (e) {
    e.preventDefault();
    e.stopPropagation();
    $(dropzoneContainer).removeClass("dragover");
    });

    dropzoneContainer.addEventListener("drop", function (e) {
        e.preventDefault();
        e.stopPropagation();
        $(dropzoneContainer).removeClass("dragover");

        let files = e.dataTransfer.files;
        fileInput.files = files;
        if(fileInput.files){
           image.src = URL.createObjectURL(files[0]);
           imageMessage.style.display = "none"
           image.style.display = "block"
           console.log(files);
        }

    });
})