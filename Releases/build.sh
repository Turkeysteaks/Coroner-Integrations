cp ../Art/icon.png .
cp ../Art/manifest.json .
cp ../README.md .
cp ../CHANGELOG.md .
cp ../CoronerBiodiversity/build/bin/Debug/*.dll ./
#Create the config directory if it doesn't already exist and copy the Strings_* files into it.
mkdir -p ./BepInEx/config/EliteMasterEric-Coroner/ && cp ../LanguageData/* ./BepInEx/config/EliteMasterEric-Coroner/

output="CoronerBiodiversity_v$(jq -r .version_number manifest.json).zip"
echo "${output}"
#Compress everything except for this file into a .zip
zip -r ./${output} ./* -x ./build.* -x ./*.zip

#jq -r .version_number manifest.json
#CAREFUL. Deletes everything in the folder except for the new zip and this file.
find ../Releases/* -not -name 'build.sh' -not -name ${output} -delete
