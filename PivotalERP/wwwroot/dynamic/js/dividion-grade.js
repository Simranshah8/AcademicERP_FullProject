on_load =()=>{

    document.getElementById('division-form').style.display="none";
    document.getElementById('grade-form').style.display="none";
  }
	window.onload=on_load();
// division section

  document.getElementById('add-division').onclick=function(){
    document.getElementById('division-section').style.display="none";
    document.getElementById('division-form').style.display="block";
}

document.getElementById('back-division').onclick = function(){
  document.getElementById('division-section').style.display="block";
  document.getElementById('division-form').style.display="none";
}

// grade section

document.getElementById('add-grade').onclick=function(){
    document.getElementById('grade-section').style.display="none";
    document.getElementById('grade-form').style.display="block";
}

document.getElementById('back-grade').onclick = function(){
  document.getElementById('grade-section').style.display="block";
  document.getElementById('grade-form').style.display="none";
}