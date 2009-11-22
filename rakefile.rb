require 'albacore'

PROJECT_ROOT = File.dirname(__FILE__)

namespace :build do

	desc "Create a debug build"
	Albacore::MSBuildTask.new(:debug) do |msb|
		outdir = File.join(PROJECT_ROOT, "build/debug/")
	  msb.properties = {:configuration => :Debug, :OutDir => outdir}
	  msb.targets [:Clean, :Build]
	  msb.solution = "src/Jamaica.sln"
	end
	
	desc "Create a debug build"
	Albacore::MSBuildTask.new(:release) do |msb|
		outdir = File.join(PROJECT_ROOT, "build/release/")
	  msb.properties = {:configuration => :Release, :OutDir => outdir}
	  msb.targets [:Clean, :Build]
	  msb.solution = "src/Jamaica.sln"
	end
	
end