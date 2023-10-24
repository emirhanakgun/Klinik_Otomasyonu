<?php
	
	include 'php/baglanti.php';
	

	session_start();
	
	
	// Check if the user clicked the logout button.
	if (isset($_POST['logout'])) {
	// Destroy the session.
	session_destroy();

	// Redirect the user to the login page.
	header("Location: /proje/login.html");
	exit();
	}
	
	
	if (!isset($_SESSION['tc_no'])) {
		// Redirect to the login page if the user is not logged in.
	header("Location: /proje/login.html");
	exit();
	}
	
	
	$tc_no=$_SESSION['tc_no'];
	
	$sql = $sql = "SELECT r.recete_no, i.ilac_adi, r.ilac_adet, r.ilac_kul_adet, r.ac_tok
        FROM recete r
        JOIN ilaclar i ON r.ilac_id = i.ilac_id
        WHERE r.tc_no = ?";;
	$params = array($tc_no);
	$stmt = sqlsrv_query($conn, $sql, $params);
	if ($stmt === false) {
	    die(print_r(sqlsrv_errors(), true));
	}







?>

<!DOCTYPE html>
<html>
<head>


<link rel="stylesheet" href="css/recete.css">
<meta charset="UTF-8">


</head>
<body>
<div class="ust_serit"></div>
<div class="header">

<div class="logo">

  <img src="img/logo.jpg" style="width:500px; height:100px; float:left;">

</div>

<a href="index.html" class="linkler">Anasayfa</a>

<a href="index2.html" class="linkler">Hakkımızda</a>

<a href="index3.html" class="linkler">Galeri</a>

<a href="index4.html" class="linkler">İletişim</a>

<a href="login.html" class="linkler" id="giris_b" style="background-color:#FF0B5C; float:right; color:white;">Giriş</a>

<a href="k_arayuz.php" class="linkler" id="k_arayuz" style="background-color:#C0EB56; color:white; float:right; ">kullanıcı</a>


</div>


<div class="orta" style="background-image: url('img/recete.jpg'); background-size: cover;">


<div id="kutu">



<h2 style="margin-left:44%;">Reçetelerim</h2>
	<table style="margin-left:10px;">
		<thead>
			<tr>
				<th style="padding-left:40px;"><h2>Reçete No</h2></th>
				<th style="padding-left:40px;"><h2>İlaç ID</h2></th>
				<th style="padding-left:40px;"><h2>İlaç adeti</h2></th>
				<th style="padding-left:40px;"><h2>İlaç Kullanım Adeti</h2></th>
				<th style="padding-left:40px;"><h2>Aç Tok</h2></th>
			</tr>
		</thead>
		<tbody>
			<?php 
			
			while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {  ?>
			<tr>
				<td style="padding-left:65px;"><?php echo $row["recete_no"]?></td>
				<td style="padding-left:65px;"><?php echo $row["ilac_adi"]; ?></td>
				<td style="padding-left:65px;"><?php echo $row["ilac_adet"]; ?></td>
				<td style="padding-left:65px;"><?php echo $row["ilac_kul_adet"]; ?></td>
				<td style="padding-left:65px;"><?php echo $row["ac_tok"]; ?></td>
			</tr>
			<?php }  ?>
		</tbody>
	</table>






</div>








</div>



<div class="footer">


</div>











</body>
</html>