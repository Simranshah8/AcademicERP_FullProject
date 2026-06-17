
  on_load =()=>{
    document.getElementById('manual-form').style.display="none";
}
	window.onload=on_load();

  // Manual billing section
  
  document.getElementById('add-manual').onclick=function(){
    document.getElementById('manual-section').style.display="none";
    document.getElementById('manual-form').style.display="block";
}
document.getElementById('manualback-btn').onclick = function(){
    document.getElementById('manual-section').style.display="block";
    document.getElementById('manual-form').style.display="none";
  }