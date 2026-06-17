on_load =()=>{
	
    document.getElementById('cas-type-form').style.display="none";
  }
  window.onload=on_load();
  // cas-type section
  
  document.getElementById('add-cas-type').onclick=function(){
    document.getElementById('cas-type-section').style.display="none";
    document.getElementById('cas-type-form').style.display="block";
}
document.getElementById('back-cas-type').onclick = function(){
    document.getElementById('cas-type-section').style.display="block";
    document.getElementById('cas-type-form').style.display="none";
  }