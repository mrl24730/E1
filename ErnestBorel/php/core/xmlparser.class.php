<?php

Class XMLparser{
	function XMLparser(){}
	
	public static function getContent($file){
		$xml = simplexml_load_file($file);
		$output = "";
		foreach($xml->active->children() as $c){
			$output .= $c->asXML();
		}

		return $output;
	}

}

?>