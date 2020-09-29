-- Copyright (C) 1991-2010 Altera Corporation
-- Your use of Altera Corporation's design tools, logic functions 
-- and other software and tools, and its AMPP partner logic 
-- functions, and any output files from any of the foregoing 
-- (including device programming or simulation files), and any 
-- associated documentation or information are expressly subject 
-- to the terms and conditions of the Altera Program License 
-- Subscription Agreement, Altera MegaCore Function License 
-- Agreement, or other applicable license agreement, including, 
-- without limitation, that your use is for the sole purpose of 
-- programming logic devices manufactured by Altera and sold by 
-- Altera or its authorized distributors.  Please refer to the 
-- applicable agreement for further details.

-- PROGRAM		"Quartus II"
-- VERSION		"Version 9.1 Build 350 03/24/2010 Service Pack 2 SJ Web Edition"
-- CREATED		"Tue Sep 29 12:09:26 2020"

LIBRARY ieee;
USE ieee.std_logic_1164.all; 

LIBRARY work;

ENTITY block1 IS 
	PORT
	(
		clk :  IN  STD_LOGIC;
		end :  OUT  STD_LOGIC
	);
END block1;

ARCHITECTURE bdf_type OF block1 IS 

COMPONENT lpm_counter5
	PORT(sclr : IN STD_LOGIC;
		 clock : IN STD_LOGIC;
		 q : OUT STD_LOGIC_VECTOR(2 DOWNTO 0)
	);
END COMPONENT;

SIGNAL	res :  STD_LOGIC_VECTOR(2 DOWNTO 0);
SIGNAL	SYNTHESIZED_WIRE_0 :  STD_LOGIC;
SIGNAL	SYNTHESIZED_WIRE_1 :  STD_LOGIC;


BEGIN 



b2v_inst : lpm_counter5
PORT MAP(sclr => SYNTHESIZED_WIRE_0,
		 clock => SYNTHESIZED_WIRE_1,
		 q => res);


SYNTHESIZED_WIRE_1 <= NOT(clk);



SYNTHESIZED_WIRE_0 <= res(0) AND res(2);


end <= NOT(res(2));



END bdf_type;