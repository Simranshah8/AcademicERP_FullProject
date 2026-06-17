on_load =()=>{

    document.getElementById('general-comment-form').style.display="none";
    document.getElementById('comment-setup-form').style.display="none";
  }
	window.onload=on_load();
// general comment section


  document.getElementById('add-general-comment').onclick=function(){
    document.getElementById('general-comment-section').style.display="none";
    document.getElementById('general-comment-form').style.display="block";
}

document.getElementById('back-general-comment').onclick = function(){
  document.getElementById('general-comment-section').style.display="block";
  document.getElementById('general-comment-form').style.display="none";
}

// comment setup section

document.getElementById('add-comment-setup').onclick=function(){
    document.getElementById('comment-setup-section').style.display="none";
    document.getElementById('comment-setup-form').style.display="block";
}

document.getElementById('back-comment-setup').onclick = function(){
  document.getElementById('comment-setup-section').style.display="block";
  document.getElementById('comment-setup-form').style.display="none";
}