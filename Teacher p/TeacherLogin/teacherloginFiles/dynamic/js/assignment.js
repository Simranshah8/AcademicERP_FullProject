on_load =()=>{
	

	document.getElementById('add-assignment-form').style.display="none";	
	document.getElementById('detail-form').style.display="none";
	
   
	
}
	window.onload=on_load();



// Add Assignment Type
document.getElementById('add-assignment-btn').onclick = function(){
		document.getElementById('add-assignment-form').style.display="block";
		document.getElementById('assignment-add-section').style.display="none";
	   
	}
	
	document.getElementById('back-add-assignment-btn').onclick = function(){
		document.getElementById('add-assignment-form').style.display="none";
		
		document.getElementById('assignment-add-section').style.display="block";
	}

	
		// / Assignment List 
	document.getElementById('show-details-btn').onclick = function(){	
	document.getElementById('assignment-list-section').style.display="none";
	document.getElementById('detail-form').style.display="block";	   
	}
		
	document.getElementById('back-assignment-list-btn').onclick = function(){
	document.getElementById('detail-form').style.display="none";		
	document.getElementById('assignment-list-section').style.display="block";
	}
	
	
	document.getElementById('exampleRadios5').onclick = function(){
		document.getElementById('markfield').style.display="block";
		
	}
	document.getElementById('exampleRadios4').onclick = function(){
		document.getElementById('markfield').style.display="none";
		
	}