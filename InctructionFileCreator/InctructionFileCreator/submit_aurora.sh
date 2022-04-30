#!/bin/bash
#SBATCH -n 196
#SBATCH -t 24:00:00
#SBATCH --mail-user=phillip.papastefanou@nateko.lu.se
#SBATCH --mail-type=ALL
#SBATCH --error=%j.err
#SBATCH --output=%j.log

mpiexec -n $SLURM_NTASKS /home/papa/src/guess4_hydraulics/build/guess  -parallel -input ncps -mdg Insfiles.txt
 
