on_load =()=>{
	document.getElementById('follow-up-required').style.display="none";
    
  }
  window.onload=on_load();
//   Followup
document.getElementById('required').onclick = function(){
	document.getElementById('follow-up-required').style.display ="block";
   
}

document.getElementById('not-required').onclick = function(){
	document.getElementById('follow-up-required').style.display="none";
	
}
