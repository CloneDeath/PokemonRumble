#!/usr/bin/env python
# -*- coding: <utf-8> -*-
# Author: Chris Mohler <cr33dog@gmail.com>
# Copyright 2009 Chris Mohler
# "Only Visible" and filename formatting introduced by mh
# Thanks to Michael Holzt for layer group code!
# Upgraded and maintained by Nicholas Rodine
# License: GPL v3+
# Version 0.6
# GIMP compatibilty 2.4.x -> 2.8.x
# GIMP plugin to export layers as PNGs

from gimpfu import *
import os, re

gettext.install("gimp20-python", gimp.locale_directory, unicode=True)

#import sys;
#sys.stderr = open( 'c:\\temp\\gimpstderr.txt', 'w');
#sys.stdout = open( 'c:\\temp\\gimpstdout.txt', 'w');

#===============================================================================
import gtk;
def MessageBox(message, title = None): # most for debugging
	dialog = gtk.MessageDialog(
		None,
		gtk.DIALOG_MODAL,
		gtk.MESSAGE_INFO,
		gtk.BUTTONS_OK,
		message
	)
	if not title:
		title = "Message"
	dialog.set_title(title)
	dialog.run()
	dialog.destroy()
#===============================================================================

def format_filename(img, layer):
	imgname = img.name.decode('utf-8')
	layername = layer.name.decode('utf-8')
	normal_regex = re.compile("[^-\w]", re.UNICODE)
	group_regex = re.compile("[^(-\\)\w]", re.UNICODE)
  
	group = "";
	clayer = layer.parent;
	while (clayer is not None):
		group = clayer.name.decode('utf-8') + "\\" + group;
		clayer = clayer.parent;
	
	filename = group + normal_regex.sub('_', layername) + '.png'
	return filename
	
def export_layer(image, layer, path, remove_offsets, crop):
	#gotta make duplicates to avoid sticking our dicks in the original
	dupe = image.duplicate()
	newlayer = layer.copy(True);
	
	newlayer.visible = 1;
	filename = format_filename(image, layer) #Use original, because the copy's name and parents are all fucked up
	fullpath = os.path.join(path, filename);
	if not os.path.exists(os.path.dirname(fullpath)):
		os.makedirs(os.path.dirname(fullpath))
		
	if (remove_offsets):
		newlayer.set_offsets(0, 0) 
	if (crop):
		pdb.plug_in_zealouscrop(dupe, newlayer)
	pdb.file_png_save(dupe, newlayer, fullpath, filename, 0, 9, 1, 1, 1, 1, 1)
	gimp.delete(dupe)

def get_layers(image, layers, path, only_visible, remove_offsets, crop):
	version = gimp.version[0:2]
	for layer in layers:
		if version[0] >= 2 and version[1] >= 8: #version 2.8 and up
			if pdb.gimp_item_is_group(layer):
				get_layers(image, layer.children, path, only_visible, remove_offsets, crop)
			else:
				if only_visible:
					if layer.visible:
						export_layer(image, layer, path, remove_offsets, crop);
				else:
					export_layer(image, layer, path, remove_offsets, crop);
		else: #version below 2.8
			if only_visible:
				if layer.visible:
					export_layer(image, layer, path, remove_offsets, crop);
			else:
				export_layer(image, layer, path, remove_offsets, crop);


def export_layers(img, drw, path, only_visible=True, remove_offsets=True, crop=True):
	dupe = img.duplicate()
	get_layers(img, img.layers, path, only_visible, remove_offsets, crop)
			
register(
	proc_name=("python-fu-export-layers"),
	blurb=("Export Layers as PNG"),
	help=("Export layers as individual PNG files."),
	author=("Nicholas Rodine"),
	copyright=("Nicholas Rodine"),
	date=("2013"),
	label=("as _PNG"),
	imagetypes=("*"),
	params=[
		(PF_IMAGE, "img", "Image", None),
		(PF_DRAWABLE, "drw", "Drawable", None),
		(PF_DIRNAME, "path", "Save PNGs here", os.getcwd()),
		(PF_BOOL, "only_visible", "Only Visible Layers?", True),
		(PF_BOOL, "remove_offsets", "Remove Offsets?", True),
		(PF_BOOL, "crop", "Zealous Crop", True),
		],
	results=[],
	function=(export_layers), 
	menu=("<Image>/File/E_xport Layers"), 
	domain=("gimp20-python", gimp.locale_directory)
	)

main()
