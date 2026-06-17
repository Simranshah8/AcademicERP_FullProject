
  on_load =()=>{
    document.getElementById('caste-form').style.display="none";
	document.getElementById('remarks-type-form').style.display="none";
    document.getElementById('document-type-form').style.display="none";
}
	window.onload=on_load();

//   caste
  document.getElementById('add-caste').onclick = function(){
	document.getElementById('caste-section').style.display ="none";
	document.getElementById('caste-form').style.display = "block";
   
}
document.getElementById('back-caste').onclick = function(){
	document.getElementById('caste-section').style.display="block";
	document.getElementById('caste-form').style.display="none";
}

// remarks type
document.getElementById('add-remarks').onclick = function(){
	document.getElementById('remarks-type-section').style.display ="none";
	document.getElementById('remarks-type-form').style.display = "block";
   
}
document.getElementById('back-remarks-type').onclick = function(){
	document.getElementById('remarks-type-section').style.display="block";
	document.getElementById('remarks-type-form').style.display="none";
}

// document type
document.getElementById('add-document').onclick = function(){
	document.getElementById('document-type-section').style.display ="none";
	document.getElementById('document-type-form').style.display = "block";
   
}
document.getElementById('back-document-type').onclick = function(){
	document.getElementById('document-type-section').style.display="block";
	document.getElementById('document-type-form').style.display="none";
}