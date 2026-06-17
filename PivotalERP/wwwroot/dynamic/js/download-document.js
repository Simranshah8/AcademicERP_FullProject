function readURL(input) {
    if (input.files && input.files[0]) {
  
      var reader = new FileReader();
  
      reader.onload = function(e) {
        $('.image-upload-wrap').hide();
  
        $('.file-upload-image').attr('src', e.target.result);
        $('.file-upload-content').show();
  
        $('.image-title').html(input.files[0].name);
      };
  
      reader.readAsDataURL(input.files[0]);
  
    } else {
      removeUpload();
    }
  }

  
  function removeUpload() {
    $('.file-upload-input-1').replaceWith($('.file-upload-input-1').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
  }
  $('.image-upload-wrap').bind('dragover', function () {
      $('.image-upload-wrap').addClass('image-dropping');
    });
    $('.image-upload-wrap').bind('dragleave', function () {
      $('.image-upload-wrap').removeClass('image-dropping');
  });


//   Export EMIS Section

on_loadclass =()=>{
    	document.getElementById('emis-form').style.display="none";
    }
    	document.onload=on_loadclass();

        document.getElementById('add-emis').onclick = function(){
            document.getElementById('emis-content').style.display ="none";
            document.getElementById('emis-form').style.display = "block";
        }
        
        document.getElementById('back-btn-emis').onclick = function(){
            document.getElementById('emis-content').style.display="block";
            document.getElementById('emis-form').style.display="none";
        }