#!/bin/bash
#SBATCH -n 1
#SBATCH -t 01:00:00
#SBATCH --mail-user=phillip.papastefanou@nateko.lu.se
#SBATCH --mail-type=ALL
#SBATCH --output=%j.log
#SBATCH --error=%j.err

current_dir=\$PWD
rm -r *run/run*
rm -r *.z*
/home/papa/src/DriverConversion/build/Converter  ""
